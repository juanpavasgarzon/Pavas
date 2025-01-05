using SharedKernel;

namespace Domain.Movements;

public static class MovementErrors
{
    public static readonly Error NotFound = Error.NotFound(
        "Movement.NotFound",
        "The provided movement was not found");
    
    public static readonly Error AlreadyCompleted = Error.Problem(
        "Movement.AlreadyCompleted",
        "The provided movement item is already completed.");
}
