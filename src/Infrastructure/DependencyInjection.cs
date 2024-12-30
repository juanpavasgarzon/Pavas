using System.Text;
using Application.Abstractions.Authentication;
using Application.Abstractions.Data;
using Application.Abstractions.Mail;
using Infrastructure.Authentication;
using Infrastructure.Authorization;
using Infrastructure.BackgroundJobs;
using Infrastructure.Database;
using Infrastructure.Interceptors;
using Infrastructure.Mail;
using Infrastructure.Settings;
using Infrastructure.Time;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using Quartz;
using SharedKernel;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);

        services.AddServices();

        services.AddJobs();

        services.AddInterceptorsInternal();

        services.AddHealthChecks(configuration);

        services.AddAuthenticationInternal();

        services.AddAuthorizationInternal();
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddSingleton<IMailSender, MailSender>();
    }

    private static void AddJobs(this IServiceCollection services)
    {
        services.AddQuartz(configurator =>
        {
            var outboxJobKey = new JobKey(nameof(ProcessOutboxMessagesJob));

            configurator.AddJob<ProcessOutboxMessagesJob>(outboxJobKey);

            configurator.AddTrigger(trigger => trigger.ForJob(outboxJobKey)
                .WithSimpleSchedule(schedule => schedule.WithIntervalInSeconds(10).RepeatForever()));
        });

        services.AddQuartzHostedService();
    }

    private static void AddInterceptorsInternal(this IServiceCollection services)
    {
        services.AddSingleton<OutboxMessageInterceptor>();
    }

    private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            OutboxMessageInterceptor outbox = serviceProvider.GetRequiredService<OutboxMessageInterceptor>();

            options.UseNpgsql(connectionString, ConfigureMigrations);

            options.AddInterceptors(outbox);

            options.UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
    }

    private static void ConfigureMigrations(NpgsqlDbContextOptionsBuilder npgsqlOptions)
    {
        npgsqlOptions.MigrationsHistoryTable(HistoryRepository.DefaultTableName, Schemas.Default);
    }

    private static void AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Database");

        services.AddHealthChecks().AddNpgSql(connectionString!);
    }

    private static void AddAuthenticationInternal(this IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            IServiceScope serviceScope = services.BuildServiceProvider().CreateScope();

            IServiceProvider serviceProvider = serviceScope.ServiceProvider;

            IOptions<JwtSettings> jwtOptions = serviceProvider.GetRequiredService<IOptions<JwtSettings>>();

            JwtSettings jwtSettings = jwtOptions.Value;

            options.RequireHttpsMetadata = false;

            options.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                ClockSkew = TimeSpan.Zero
            };
        });

        services.AddHttpContextAccessor();

        services.AddScoped<IUserContext, UserContext>();

        services.AddSingleton<IPasswordHasher, PasswordHasher>();

        services.AddSingleton<ITokenProvider, TokenProvider>();
    }

    private static void AddAuthorizationInternal(this IServiceCollection services)
    {
        services.AddAuthorization();

        services.AddScoped<PermissionProvider>();

        services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();

        services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();
    }
}
