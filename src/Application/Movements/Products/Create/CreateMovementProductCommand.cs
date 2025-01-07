using Application.Abstractions.Messaging;

namespace Application.Movements.Products.Create;

public sealed record CreateMovementProductCommand(
    Guid MovementId,
    Guid ProductId,
    decimal Quantity,
    string Notes) : ICommand;

