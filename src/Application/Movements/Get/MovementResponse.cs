namespace Application.Movements.Get;

public sealed class MovementResponse
{
    public Guid Id { get; set; }
    public string Reference { get; set; }
    public string Type { get; set; }
    public string Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CompletedAt { get; set; }
}
