using Application.Abstractions.Messaging;
using Domain.Todos;

namespace Application.Todos.Create;

public sealed record CreateTodoCommand(
    Guid UserId,
    string Description,
    DateTime? DueDate,
    List<string> Labels,
    Priority Priority) : ICommand<Guid>;
