using SharedKernel;

namespace Domain.Products;

public static class ProductErrors
{
    public static Error NotFound(Guid productId) => Error.NotFound(
        "Product.NotFound",
        $"The product with the Id = '{productId}' was not found");
    
    public static readonly Error CodeNotUnique = Error.Conflict(
        "Product.CodeNotUnique",
        "The provided code is not unique");
}
