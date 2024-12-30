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
}
