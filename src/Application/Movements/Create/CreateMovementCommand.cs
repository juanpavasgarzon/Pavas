using Application.Abstractions.Messaging;
using Domain.Movements;

namespace Application.Movements.Create;

public sealed record CreateMovementCommand(
    string Reference,
    MovementType Type,
    string Notes) : ICommand<Guid>;
