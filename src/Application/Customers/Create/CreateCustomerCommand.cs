using Application.Abstractions.Messaging;

namespace Application.Customers.Create;

public sealed record CreateCustomerCommand(
    string Id,
    string Name,
    string Email,
    string Phone,
    string Address,
    string Notes) : ICommand;
