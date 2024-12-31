using Application.Abstractions.Messaging;

namespace Application.Products.Delete;

public sealed record DeleteProductCommand(Guid ProductId) : ICommand;
