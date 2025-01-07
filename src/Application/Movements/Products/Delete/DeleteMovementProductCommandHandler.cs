using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Movements;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Movements.Products.Delete;

internal sealed class DeleteMovementProductCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<DeleteMovementProductCommand>
{
    public async Task<Result> Handle(DeleteMovementProductCommand command, CancellationToken cancellationToken)
    {
        MovementProduct? movementProduct = await context.MovementProducts
            .Include(mp => mp.Movement)
            .SingleOrDefaultAsync(mp =>
                mp.MovementId == command.MovementId && mp.ProductId == command.ProductId, cancellationToken);

        if (movementProduct is null)
        {
            return Result.Failure(MovementErrors.NotFoundProduct);
        }

        if (movementProduct.Movement.IsCompleted)
        {
            return Result.Failure(MovementErrors.CannotDeleteProduct);
        }

        context.MovementProducts.Remove(movementProduct);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
