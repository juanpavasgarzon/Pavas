using Infrastructure.Settings;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api;

public static class DependencyInjection
{
    public static void AddPresentation(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGenWithAuth();

        services.AddExceptionHandler<GlobalExceptionHandler>();

        services.AddProblemDetails();

        services.AddSettings();

        services.AddSpecificOriginCors();
    }

    private static void AddSettings(this IServiceCollection services)
    {
        services.AddOptions<JwtSettings>()
            .BindConfiguration(JwtSettings.Path)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddOptions<MailSettings>()
            .BindConfiguration(MailSettings.Path)
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }

    private static void AddSpecificOriginCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin", policy =>
            {
                policy.WithOrigins("http://localhost:5173")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
            });
        });
    }
}
