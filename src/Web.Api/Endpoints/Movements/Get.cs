using Application.Movements.Delete;
using Application.Movements.Get;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Movements;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("movements", Handle)
            .WithTags(Tags.Movements)
            .RequireAuthorization();
    }

    private static async Task<IResult> Handle(ISender sender, CancellationToken cancellationToken)
    {
        var query = new GetMovementsQuery();

        Result<List<MovementResponse>> result = await sender.Send(query, cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
