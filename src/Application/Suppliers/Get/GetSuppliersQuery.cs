using Application.Abstractions.Messaging;

namespace Application.Suppliers.Get;

public sealed class GetSuppliersQuery : IQuery<List<SupplierResponse>>;
