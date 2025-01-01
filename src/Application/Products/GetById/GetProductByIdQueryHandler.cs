using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Products;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Products.GetById;

internal sealed class GetProductByIdQueryHandler(
    IApplicationDbContext context
) : IQueryHandler<GetProductByIdQuery, ProductResponse>
{
    public async Task<Result<ProductResponse>> Handle(GetProductByIdQuery query,
        CancellationToken cancellationToken)
    {
        ProductResponse? product = await context.Products
            .Where(p => p.Id == query.ProductId)
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
            .SingleOrDefaultAsync(cancellationToken);

        return product ?? Result.Failure<ProductResponse>(ProductErrors.NotFound);
    }
}
