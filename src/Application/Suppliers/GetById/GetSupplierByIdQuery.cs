using Application.Abstractions.Messaging;
using Application.Suppliers.Get;

namespace Application.Suppliers.GetById;

public sealed record GetSupplierByIdQuery(string SupplierId) : IQuery<SupplierResponse>;
