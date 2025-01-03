using Application.Abstractions.Messaging;

namespace Application.Suppliers.GetById;

public sealed record GetSupplierByIdQuery(string SupplierId) : IQuery<SupplierResponse>;
