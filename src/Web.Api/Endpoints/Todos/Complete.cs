using Application.Todos.Complete;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Todos;

internal sealed class Complete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("todos/{todoId:guid}/complete", Handle)
            .WithTags(Tags.Todos)
            .RequireAuthorization();
    }

    private static async Task<IResult> Handle(Guid todoId, ISender sender, CancellationToken cancellationToken)
    {
        var command = new CompleteTodoCommand(todoId);

        Result result = await sender.Send(command, cancellationToken);

        return result.Match(Results.NoContent, CustomResults.Problem);
    }
}
