using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Movements;
using Domain.Products;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Movements.Products.Create;

public sealed class CreateMovementProductCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<CreateMovementProductCommand>
{
    public async Task<Result> Handle(CreateMovementProductCommand command, CancellationToken cancellationToken)
    {
        Movement? movement = await context.Movements.AsNoTracking()
            .SingleOrDefaultAsync(m => m.Id == command.MovementId, cancellationToken);

        if (movement is null)
        {
            return Result.Failure(MovementErrors.NotFound);
        }

        if (movement.IsCompleted)
        {
            return Result.Failure(MovementErrors.CannotCreateProduct);
        }

        Product? product = await context.Products.AsNoTracking()
            .SingleOrDefaultAsync(p => p.Id == command.ProductId, cancellationToken);

        if (product is null)
        {
            return Result.Failure(ProductErrors.NotFound);
        }

        var movementProduct = new MovementProduct
        {
            MovementId = movement.Id,
            ProductId = product.Id,
            Quantity = command.Quantity,
            Notes = command.Notes
        };

        context.MovementProducts.Add(movementProduct);
        
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
