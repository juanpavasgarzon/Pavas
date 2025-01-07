using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Movements;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Movements.Delete;

internal sealed class DeleteMovementCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<DeleteMovementCommand>
{
    public async Task<Result> Handle(DeleteMovementCommand command, CancellationToken cancellationToken)
    {
        Movement? movement = await context.Movements
            .Include(m => m.MovementProducts)
            .Where(m => m.Id == command.MovementId)
            .SingleOrDefaultAsync(cancellationToken);

        if (movement is null)
        {
            return Result.Failure(MovementErrors.NotFound);
        }

        if (movement.IsCompleted)
        {
            return Result.Failure(MovementErrors.CannotDelete);
        }

        context.Movements.Remove(movement);

        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
