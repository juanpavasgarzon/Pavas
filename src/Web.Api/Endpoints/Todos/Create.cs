using Application.Todos.Create;
using Domain.Todos;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Todos;

internal sealed class Create : IEndpoint
{
    private sealed record Request(
        Guid UserId,
        string Description,
        int Priority,
        DateTime? DueDate,
        List<string> Labels
    );

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("todos", Handle)
            .WithTags(Tags.Todos)
            .RequireAuthorization();
    }

    private static async Task<IResult> Handle(Request request, ISender sender, CancellationToken cancellationToken)
    {
        var command = new CreateTodoCommand(
            request.UserId,
            request.Description,
            request.DueDate,
            request.Labels,
            (Priority)request.Priority);

        Result<Guid> result = await sender.Send(command, cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
