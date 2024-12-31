using Application.Customers.GetById;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Customers;

public class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("customers/{id}", Handle)
            .WithTags(Tags.Customers)
            .RequireAuthorization();
    }

    private static async Task<IResult> Handle(string id, ISender sender, CancellationToken cancellationToken)
    {
        var query = new GetCustomerByIdQuery(id);

        Result<CustomerResponse> result = await sender.Send(query, cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
