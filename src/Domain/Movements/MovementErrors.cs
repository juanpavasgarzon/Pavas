using SharedKernel;

namespace Domain.Movements;

public static class MovementErrors
{
    public static readonly Error NotFound = Error.NotFound(
        "Movement.NotFound",
        "The provided movement was not found");

    public static readonly Error CannotComplete = Error.Problem(
        "Movement.CannotComplete",
        "The movement cannot be completed at this stage.");

    public static readonly Error AlreadyCompleted = Error.Problem(
        "Movement.AlreadyCompleted",
        "The provided movement is already completed.");
    
    public static readonly Error CannotDelete = Error.Problem(
        "Movement.CannotDelete",
        "The movement cannot be delete at this stage.");
    
    public static readonly Error NotFoundProduct = Error.NotFound(
        "MovementProduct.NotFound",
        "The provided movement product was not found");
    
    public static readonly Error CannotDeleteProduct = Error.Problem(
        "MovementProduct.CannotDelete",
        "The movement product cannot be delete at this stage.");
    
    public static readonly Error CannotCreateProduct = Error.Problem(
        "MovementProduct.CannotCreate",
        "The movement product cannot be create at this stage.");
}
