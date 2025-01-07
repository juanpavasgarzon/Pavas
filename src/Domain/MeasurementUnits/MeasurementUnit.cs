using Domain.Products;
using SharedKernel;

namespace Domain.MeasurementUnits;

public sealed class MeasurementUnit : Entity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<Product> Products { get; set; }
}
