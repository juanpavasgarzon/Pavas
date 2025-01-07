using Application.Movements.Products.Create;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Movements.Products;

internal sealed class Create : IEndpoint
{
    private sealed record Request(Guid ProductId, decimal Quantity, string Notes);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("movements/{movementId:guid}/products", Handle)
            .WithTags(Tags.Movements)
            .RequireAuthorization();
    }

    private static async Task<IResult> Handle(Guid movementId, Request request, ISender sender,
        CancellationToken cancellationToken)
    {
        var command = new CreateMovementProductCommand(movementId, request.ProductId, request.Quantity, request.Notes);

        Result result = await sender.Send(command, cancellationToken);

        return result.Match(Results.NoContent, CustomResults.Problem);
    }
}
