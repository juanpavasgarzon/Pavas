using SharedKernel;

namespace Domain.Products;

public static class ProductErrors
{
    public static readonly Error NotFound = Error.NotFound(
        "Product.NotFound",
        "The provided product was not found");

    public static readonly Error CanNotDelete = Error.Problem(
        "Product.CanNotDelete",
        "The provided product can't delete");

    public static readonly Error CodeNotUnique = Error.Conflict(
        "Product.CodeNotUnique",
        "The provided product code is not unique");
}
