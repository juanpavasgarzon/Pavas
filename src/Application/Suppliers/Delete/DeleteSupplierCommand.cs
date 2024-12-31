using Application.Abstractions.Messaging;

namespace Application.Suppliers.Delete;

public sealed record DeleteSupplierCommand(string SupplierId) : ICommand;
