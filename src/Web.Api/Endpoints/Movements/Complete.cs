using Application.Movements.Complete;
using Application.Movements.Create;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Movements;

internal sealed class Complete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("movements/{movementId:guid}/complete", Handle)
            .WithTags(Tags.Movements)
            .RequireAuthorization();
    }

    private static async Task<IResult> Handle(Guid movementId, ISender sender, CancellationToken cancellationToken)
    {
        var command = new CompleteMovementCommand(movementId);

        Result result = await sender.Send(command, cancellationToken);

        return result.Match(Results.NoContent, CustomResults.Problem);
    }
}
