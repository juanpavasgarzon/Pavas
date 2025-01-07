using Application.Movements.Create;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Movements;

internal sealed class Create : IEndpoint
{
    private sealed record Request(string Reference, string Type, string Notes);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("movements", Handle)
            .WithTags(Tags.Movements)
            .RequireAuthorization();
    }

    private static async Task<IResult> Handle(Request request, ISender sender, CancellationToken cancellationToken)
    {
        var command = new CreateMovementCommand(request.Reference, request.Type, request.Notes);

        Result<Guid> result = await sender.Send(command, cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
