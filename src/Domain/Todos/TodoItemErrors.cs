using SharedKernel;

namespace Domain.Todos;

public static class TodoItemErrors
{
    public static readonly Error AlreadyCompleted = Error.Problem(
        "TodoItems.AlreadyCompleted",
        "The provided to-do item is already completed.");

    public static readonly Error NotFound = Error.NotFound(
        "TodoItems.NotFound",
        "The provided to-do item was not found");
}
