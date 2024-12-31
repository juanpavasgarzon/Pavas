using Application.Suppliers.GetById;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Suppliers;

public class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("suppliers/{id}", Handle)
            .WithTags(Tags.Suppliers)
            .RequireAuthorization();
    }

    private static async Task<IResult> Handle(string id, ISender sender, CancellationToken cancellationToken)
    {
        var query = new GetSupplierByIdQuery(id);

        Result<SupplierResponse> result = await sender.Send(query, cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
