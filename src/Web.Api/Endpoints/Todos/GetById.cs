﻿using Application.Todos.GetById;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Todos;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("todos/{id:guid}", Handle)
            .WithTags(Tags.Todos)
            .RequireAuthorization();
    }

    private static async Task<IResult> Handle(Guid id, ISender sender, CancellationToken cancellationToken)
    {
        var query = new GetTodoByIdQuery(id);

        Result<TodoResponse> result = await sender.Send(query, cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}
