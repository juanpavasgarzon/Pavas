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
    public static void AddInfrastructure(this IServiceCollection services)
    {
        services.AddDatabase();

        services.AddServices();

        services.AddJobs();

        services.AddInterceptorsInternal();

        services.AddHealths();

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
                .WithSimpleSchedule(schedule => schedule.WithIntervalInSeconds(5).RepeatForever()));
        });

        services.AddQuartzHostedService();
    }

    private static void AddInterceptorsInternal(this IServiceCollection services)
    {
        services.AddSingleton<OutboxMessageInterceptor>();
    }

    private static void AddDatabase(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();

            string? connectionString = configuration.GetConnectionString("Database");

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

    private static void AddHealths(this IServiceCollection services)
    {
        using IServiceScope serviceScope = services.BuildServiceProvider().CreateScope();
        
        IConfiguration configuration = serviceScope.ServiceProvider.GetRequiredService<IConfiguration>();

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
