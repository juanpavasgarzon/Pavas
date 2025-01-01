using SharedKernel;

namespace Domain.Customers;

public static class CustomerErrors
{
    public static readonly Error EmailNotUnique = Error.Conflict(
        "Customer.EmailNotUnique",
        "The provided customer email is not unique");

    public static readonly Error IdNotUnique = Error.Conflict(
        "Customer.IdNotUnique",
        "The provided customer id is not unique");

    public static readonly Error NotFound = Error.NotFound(
        "Customer.NotFound",
        "The provider customer was not found");

    public static readonly Error CanNotDelete = Error.Problem(
        "Customer.CanNotDelete",
        "The provider customer can't delete");
}
