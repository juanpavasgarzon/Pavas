using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Products.Get;

internal sealed class GetProductsQueryHandler(
    IApplicationDbContext context
) : IQueryHandler<GetProductsQuery, List<ProductResponse>>
{
    public async Task<Result<List<ProductResponse>>> Handle(GetProductsQuery query, CancellationToken cancellationToken)
    {
        List<ProductResponse> products = await context.Products
            .Select(product => new ProductResponse
            {
                Id = product.Id,
                Code = product.Code,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CreatedAt = product.CreatedAt
            })
            .ToListAsync(cancellationToken);

        return products;
    }
}
