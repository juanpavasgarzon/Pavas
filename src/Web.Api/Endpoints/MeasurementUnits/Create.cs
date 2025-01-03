using Application.MeasurementUnits.Create;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.MeasurementUnits;

internal sealed class Create : IEndpoint
{
    private sealed record Request(string Name, string Symbol);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("measurement-unit", Handle)
            .WithTags(Tags.MeasurementUnit)
            .RequireAuthorization();
    }

    private static async Task<IResult> Handle(Request request, ISender sender, CancellationToken cancellationToken)
    {
        var command = new CreateMeasurementUnitCommand(request.Name, request.Symbol);

        Result<Guid> result = await sender.Send(command, cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
