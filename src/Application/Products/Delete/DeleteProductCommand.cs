using Application.Abstractions.Messaging;

namespace Application.Products.Delete;

public record DeleteProductCommand(Guid ProductId) : ICommand;
