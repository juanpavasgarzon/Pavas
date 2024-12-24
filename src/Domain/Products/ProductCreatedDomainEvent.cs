using SharedKernel;

namespace Domain.Products;

public sealed record ProductCreatedDomainEvent(Guid ProductId) : IDomainEvent;
