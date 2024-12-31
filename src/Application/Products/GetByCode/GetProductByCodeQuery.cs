using Application.Abstractions.Messaging;

namespace Application.Products.GetByCode;

public sealed record GetProductByCodeQuery(string Code) : IQuery<ProductResponse>;
