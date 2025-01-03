using Application.Customers.Get;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Customers;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("customers", Handle)
            .WithTags(Tags.Customers)
            .RequireAuthorization();
    }

    private static async Task<IResult> Handle(ISender sender, CancellationToken cancellationToken)
    {
        var query = new GetCustomersQuery();

        Result<List<CustomerResponse>> result = await sender.Send(query, cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
