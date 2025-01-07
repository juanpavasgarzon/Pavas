using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Movements;
using SharedKernel;

namespace Application.Movements.Create;

internal sealed class CreateMovementCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider
) : ICommandHandler<CreateMovementCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateMovementCommand command, CancellationToken cancellationToken)
    {
        var movement = new Movement
        {
            Id = Guid.NewGuid(),
            Reference = command.Reference,
            Type = Enum.Parse<MovementType>(command.Type),
            Notes = command.Notes,
            CreatedAt = dateTimeProvider.UtcNow,
            IsCompleted = false
        };

        context.Movements.Add(movement);

        await context.SaveChangesAsync(cancellationToken);

        return movement.Id;
    }
}
