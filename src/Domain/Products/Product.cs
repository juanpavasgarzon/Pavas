using SharedKernel;

namespace Domain.Products;

public sealed class Product : Entity
{
    public Guid Id { get; set; }
    public string SupplierId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid MeasurementUnitId { get; set; }
    public decimal Price { get; set; }
    public decimal StockQuantity { get; set; }
    public DateTime CreatedAt { get; set; }
}
