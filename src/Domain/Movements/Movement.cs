using SharedKernel;

namespace Domain.Movements;

public sealed class Movement : Entity
{
    public Guid Id { get; set; }
    public string Reference { get; set; }
    public MovementType Type { get; set; }
    public string Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CompletedAt { get; set; }
}
