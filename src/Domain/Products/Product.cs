using Domain.MeasurementUnits;
using Domain.Movements;
using Domain.Suppliers;
using SharedKernel;

namespace Domain.Products;

public sealed class Product : Entity
{
    public Guid Id { get; set; }
    public string SupplierId { get; set; }
    public Supplier Supplier { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid MeasurementUnitId { get; set; }
    public decimal Price { get; set; }
    public decimal StockQuantity { get; set; }
    public DateTime CreatedAt { get; set; }
    public MeasurementUnit MeasurementUnit { get; set; }
    public ICollection<MovementProduct> MovementProducts { get; set; }
}
