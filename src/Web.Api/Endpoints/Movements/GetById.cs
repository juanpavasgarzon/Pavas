using Application.Movements.GetById;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Movements;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("movements/{id:guid}", Handle)
            .WithTags(Tags.Movements)
            .RequireAuthorization();
    }

    private static async Task<IResult> Handle(Guid id, ISender sender, CancellationToken cancellationToken)
    {
        var query = new GetMovementByIdQuery(id);

        Result<MovementResponse> result = await sender.Send(query, cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
