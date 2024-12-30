using Infrastructure.BackgroundJobs;
using Infrastructure.Settings;
using Quartz;
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

        services.AddJobs();

        services.AddSettings();
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
