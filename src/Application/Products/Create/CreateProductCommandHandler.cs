using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.MeasurementUnits;
using Domain.Products;
using Domain.Suppliers;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Products.Create;

internal sealed class CreateProductCommandHandler(
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

        MeasurementUnit? measurementUnit = await context.MeasurementUnits.AsNoTracking()
            .SingleOrDefaultAsync(u => u.Id == command.MeasurementUnitId, cancellationToken);

        if (measurementUnit is null)
        {
            return Result.Failure<Guid>(MeasurementUnitErrors.NotFound);
        }

        Supplier? supplier = await context.Suppliers.AsNoTracking()
            .SingleOrDefaultAsync(s => s.Id == command.SupplierId, cancellationToken);

        if (supplier is null)
        {
            return Result.Failure<Guid>(SupplierErrors.NotFound);
        }

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Code = command.Code,
            Name = command.Name,
            Description = command.Description,
            SupplierId = supplier.Id,
            MeasurementUnitId = measurementUnit.Id,
            Price = command.Price,
            StockQuantity = decimal.Zero,
            CreatedAt = dateTimeProvider.UtcNow
        };

        context.Products.Add(product);

        await context.SaveChangesAsync(cancellationToken);

        return product.Id;
    }
}
