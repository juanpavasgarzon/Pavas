namespace Application.Products.GetById;

public sealed class ProductResponse
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public decimal StockQuantity { get; set; }
    public DateTime CreatedAt { get; set; }
}
