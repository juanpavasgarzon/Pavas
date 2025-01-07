using Application.Abstractions.Messaging;

namespace Application.Movements.Create;


public sealed record CreateMovementCommand(
    string Reference,
    string Type,
    string Notes) : ICommand<Guid>;
