using Application.MeasurementUnits.GetBySymbol;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.MeasurementUnits;

internal sealed class GetBySymbol : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("measurement-unit/symbol/{symbol}", Handle)
            .WithTags(Tags.MeasurementUnit)
            .RequireAuthorization();
    }

    private static async Task<IResult> Handle(string symbol, ISender sender, CancellationToken cancellationToken)
    {
        var query = new GetMeasurementUnitBySymbolQuery(symbol);

        Result<MeasurementUnitResponse> result = await sender.Send(query, cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
