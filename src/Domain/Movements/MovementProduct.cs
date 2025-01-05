using Domain.Products;
using SharedKernel;

namespace Domain.Movements;

public sealed class MovementProduct : Entity
{
    public Guid MovementId { get; set; }
    public Movement Movement { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public decimal Quantity { get; set; }
    public string Notes { get; set; }
}
