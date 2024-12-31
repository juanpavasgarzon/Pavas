using Application.Abstractions.Messaging;

namespace Application.Products.Get;

public sealed record GetProductsQuery : IQuery<List<ProductResponse>>;
