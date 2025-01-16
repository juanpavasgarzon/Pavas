using Application;
using HealthChecks.UI.Client;
using Infrastructure;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using Web.Api;
using Web.Api.Extensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.AddSerilog();

builder.Services.AddOpenApi();

builder.Services.AddPresentation();

builder.Services.AddApplication();

builder.Services.AddInfrastructure();

builder.Services.AddEndpoints();

WebApplication app = builder.Build();

app.UseCors("AllowSpecificOrigin");

app.MapEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerWithUi();

    app.ApplyMigrations();
}

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseRequestContextLogging();

app.UseSerilogRequestLogging();

app.UseExceptionHandler();

app.UseAuthentication();

app.UseAuthorization();

await app.RunAsync();

namespace Web.Api
{
    public partial class Program;
}
