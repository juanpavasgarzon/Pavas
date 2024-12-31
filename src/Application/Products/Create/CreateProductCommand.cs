using Application.Abstractions.Messaging;

namespace Application.Products.Create;

public sealed record CreateProductCommand(
    string Code,
    string Name,
    string Description,
    decimal Price) : ICommand<Guid>;
