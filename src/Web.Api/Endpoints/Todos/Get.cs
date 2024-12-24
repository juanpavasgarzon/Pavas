﻿using Application.Todos.Get;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;
using Web.Api.Infrastructure;

namespace Web.Api.Endpoints.Todos;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("todos", Handle)
            .WithTags(Tags.Todos)
            .RequireAuthorization();
    }

    private static async Task<IResult> Handle(Guid userId, ISender sender, CancellationToken cancellationToken)
    {
        var command = new GetTodosQuery(userId);

        Result<List<TodoResponse>> result = await sender.Send(command, cancellationToken);

        return result.Match(Results.Ok, CustomResults.Problem);
    }
}