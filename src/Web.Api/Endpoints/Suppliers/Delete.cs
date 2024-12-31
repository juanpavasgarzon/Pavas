using Application.Suppliers.Delete;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Suppliers;

public class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("suppliers/{id}", Handle)
            .WithTags(Tags.Suppliers)
            .RequireAuthorization();
    }

    private static async Task<IResult> Handle(string id, ISender sender, CancellationToken cancellationToken)
    {
        var command = new DeleteSupplierCommand(id);

        Result result = await sender.Send(command, cancellationToken);

        return result.Match(Results.NoContent, CustomResults.Problem);
    }
}
