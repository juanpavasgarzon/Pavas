using Application.Abstractions.Messaging;

namespace Application.Customers.GetById;

public sealed record GetCustomerByIdQuery(string CustomerId) : IQuery<CustomerResponse>;
