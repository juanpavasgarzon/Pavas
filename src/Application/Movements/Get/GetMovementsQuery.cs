using Application.Abstractions.Messaging;

namespace Application.Movements.Get;

public sealed record GetMovementsQuery : IQuery<List<MovementResponse>>;
