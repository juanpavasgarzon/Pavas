using SharedKernel;

namespace Domain.Movements;

public sealed class MovementProduct : Entity
{
    public Guid MovementId { get; set; }
    public Guid ProductId { get; set; }
    public decimal Quantity { get; set; }
    public string Notes { get; set; }
}
