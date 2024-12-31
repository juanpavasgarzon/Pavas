using Application.Customers.Delete;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Customers;

public class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("customers/{id}", Handle)
            .WithTags(Tags.Customers)
            .RequireAuthorization();
    }

    private static async Task<IResult> Handle(string id, ISender sender, CancellationToken cancellationToken)
    {
        var command = new DeleteCustomerCommand(id);

        Result result = await sender.Send(command, cancellationToken);

        return result.Match(Results.NoContent, CustomResults.Problem);
    }
}
