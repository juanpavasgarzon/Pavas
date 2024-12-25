using Application.Abstractions.Messaging;

namespace Application.Products.Get;

public record GetProductsQuery : IQuery<List<ProductResponse>>;
