using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Movements;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.Movements.Complete;

internal sealed class CompleteMovementCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider
) : ICommandHandler<CompleteMovementCommand>
{
    public async Task<Result> Handle(CompleteMovementCommand command, CancellationToken cancellationToken)
    {
        Movement? movement = await context.Movements
            .SingleOrDefaultAsync(m => m.Id == command.MovementId, cancellationToken);

        if (movement is null)
        {
            return Result.Failure(MovementErrors.NotFound);
        }

        if (movement.IsCompleted)
        {
            return Result.Failure(MovementErrors.AlreadyCompleted);
        }

        movement.IsCompleted = true;
        movement.CompletedAt = dateTimeProvider.UtcNow;
        
        await context.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
