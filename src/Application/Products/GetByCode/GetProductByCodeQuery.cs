using Application.Abstractions.Messaging;

namespace Application.Products.GetByCode;

public record GetProductByCodeQuery(string Code) : IQuery<ProductResponse>;
