using Application.Abstractions.Messaging;

namespace Application.Movements.Products.Delete;

public sealed record DeleteMovementProductCommand(Guid MovementId, Guid ProductId): ICommand;
