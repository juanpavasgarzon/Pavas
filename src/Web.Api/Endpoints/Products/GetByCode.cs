using Application.Products.GetByCode;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Products;

internal sealed class GetByCode : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("products/{code}", Handle)
            .WithTags(Tags.Products)
            .RequireAuthorization();
    }

    private static async Task<IResult> Handle(string code, ISender sender, CancellationToken cancellationToken)
    {
        var query = new GetProductByCodeQuery(code);

        Result<ProductResponse> result = await sender.Send(query, cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
