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
}
