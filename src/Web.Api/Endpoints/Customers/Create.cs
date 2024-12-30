using Application.Customers.Create;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Customers;

internal sealed class Create : IEndpoint
{
    private sealed record Request(
        string Id,
        string Name,
        string Email,
        string Phone,
        string Address,
        string Notes
    );

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("customers", Handle)
            .WithTags(Tags.Customers)
            .RequireAuthorization();
    }

    private static async Task<IResult> Handle(Request request, ISender sender, CancellationToken cancellationToken)
    {
        var command = new CreateCustomerCommand
        {
            Id = request.Id,
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
            Address = request.Address,
            Notes = request.Notes,
        };

        Result result = await sender.Send(command, cancellationToken);

        return result.Match(Results.NoContent, CustomResults.Problem);
    }
}