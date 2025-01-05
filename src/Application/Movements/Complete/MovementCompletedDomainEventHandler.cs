using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Movements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace Application.Movements.Complete;

internal sealed class MovementCompletedDomainEventHandler(
    IApplicationDbContext context,
    ILogger<MovementCompletedDomainEventHandler> logger
) : IDomainEventHandler<MovementCompletedDomainEvent>
{
    public async Task Handle(MovementCompletedDomainEvent notification, CancellationToken cancellationToken)
    {
        Movement? movement = await context.Movements
            .Include(m => m.MovementProducts)
            .ThenInclude(mp => mp.Product)
            .AsNoTracking()
            .Where(m => m.Id == notification.MovementId)
            .SingleOrDefaultAsync(cancellationToken);

        if (movement is null)
        {
            logger.LogError("Movement with Id {MovementId} Not Found", notification.MovementId);
            return;
        }

        await using IDbContextTransaction transaction = await context.GetDatabase()
            .BeginTransactionAsync(cancellationToken);

        try
        {
            foreach (MovementProduct movementProduct in movement.MovementProducts)
            {
                movementProduct.Product.StockQuantity += movementProduct.Quantity;
            }

            await context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            logger.LogError(exception, "Error processing movement {MovementId}.", notification.MovementId);
        }
    }
}
