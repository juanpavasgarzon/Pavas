using Application.Abstractions.Messaging;

namespace Application.Products.Create;

public class CreateProductCommand : ICommand<Guid>
{
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}
