using Application.Abstractions.Messaging;

namespace Application.Movements.Complete;

public sealed record CompleteMovementCommand(Guid MovementId) : ICommand;
