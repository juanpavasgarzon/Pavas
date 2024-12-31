using Application.Abstractions.Messaging;

namespace Application.Customers.Get;

public sealed record GetCustomersQuery : IQuery<List<CustomerResponse>>;
