using SharedKernel;

namespace Domain.Movements;

public sealed record MovementCompletedDomainEvent(Guid MovementId) : IDomainEvent;
