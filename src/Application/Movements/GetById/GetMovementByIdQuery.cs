using Application.Abstractions.Messaging;

namespace Application.Movements.GetById;

public sealed record GetMovementByIdQuery(Guid MovementId) : IQuery<MovementResponse>;
