using Application.Abstractions.Messaging;

namespace Application.Products.GetById;

public record GetProductByIdQuery(Guid ProductId) : IQuery<ProductResponse>;
