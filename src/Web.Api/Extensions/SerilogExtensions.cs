using Serilog;

namespace Web.Api.Extensions;

public static class SerilogExtensions
{
    public static void AddSerilog(this ConfigureHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog((context, loggerConfig) =>
        {
            loggerConfig.ReadFrom.Configuration(context.Configuration);
        });
    }
}
