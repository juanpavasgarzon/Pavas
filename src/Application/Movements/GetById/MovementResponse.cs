namespace Application.Movements.GetById;

public sealed class ProductResponse
{
    public Guid ProductId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public decimal Quantity { get; set; }
    public string Notes { get; set; }
}

public sealed class MovementResponse
{
    public Guid Id { get; set; }
    public string Reference { get; set; }
    public string Type { get; set; }
    public string Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime? CompletedAt { get; set; }
    public List<ProductResponse> Products { get; set; }
}
