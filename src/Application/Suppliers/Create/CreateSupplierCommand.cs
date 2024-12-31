using Application.Abstractions.Messaging;

namespace Application.Suppliers.Create;

public sealed record CreateSupplierCommand(
    string Id,
    string Name,
    string Email,
    string Phone,
    string Address,
    string Notes) : ICommand;
