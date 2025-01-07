using Application.Abstractions.Messaging;

namespace Application.Movements.Delete;

public sealed record DeleteMovementCommand(Guid MovementId) : ICommand;
