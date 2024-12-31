using SharedKernel;

namespace Domain.Customers;

public static class CustomerErrors
{
    public static readonly Error EmailNotUnique = Error.Conflict(
        "Customer.EmailNotUnique",
        "The provided email is not unique");
    
    public static readonly Error IdNotUnique = Error.Conflict(
        "Customer.IdNotUnique",
        "The provided id is not unique");
    
    public static Error NotFound(string id) => Error.NotFound(
        "Customer.NotFound",
        $"The customer with the id = '{id}' was not found");
    
    public static Error CanNotDelete(string name) => Error.Problem(
        "Customer.CanNotDelete",
        $"The customer with the Name = '{name}' can't delete");

}
