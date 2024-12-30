using Application.Products.Create;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Products;

internal sealed class Create : IEndpoint
{
    private sealed record Request(
        string Code, 
        string Name,
        string Description,
        decimal Price
    );

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("products", Handle)
            .WithTags(Tags.Products)
            .RequireAuthorization();
    }

    private static async Task<IResult> Handle(Request request, ISender sender, CancellationToken cancellationToken)
    {
        var command = new CreateProductCommand
        {
            Code = request.Code,
            Name = request.Name,
            Description = request.Description,
            Price = request.Price
        };

        Result<Guid> result = await sender.Send(command, cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
