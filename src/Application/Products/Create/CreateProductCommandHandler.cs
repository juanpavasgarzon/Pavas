using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Products;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Products.Create;

internal class CreateProductCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider
) : ICommandHandler<CreateProductCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        if (await context.Products.AnyAsync(p => p.Code == command.Code, cancellationToken))
        {
            return Result.Failure<Guid>(ProductErrors.CodeNotUnique);
        }

        var product = new Product
        {
            Code = command.Code,
            Name = command.Name,
            Description = command.Description,
            Price = command.Price,
            StockQuantity = 0,
            CreatedAt = dateTimeProvider.UtcNow
        };

        context.Products.Add(product);

        await context.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
