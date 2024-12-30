using SharedKernel;

namespace Domain.Suppliers;

public static class SupplierErrors
{
    public static readonly Error EmailNotUnique = Error.Conflict(
        "Supplier.EmailNotUnique",
        "The provided email is not unique");
    
    public static readonly Error IdNotUnique = Error.Conflict(
        "Supplier.IdNotUnique",
        "The provided id is not unique");
}
