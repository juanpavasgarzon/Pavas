using Application.Suppliers.Create;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Suppliers;

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
        app.MapPost("suppliers", Handle)
            .WithTags(Tags.Suppliers)
            .RequireAuthorization();
    }

    private static async Task<IResult> Handle(Request request, ISender sender, CancellationToken cancellationToken)
    {
        var command = new CreateSupplierCommand
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