namespace Domain.Purchases;

public class Purchase
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Status Status { get; set; }
    public decimal TotalAmount { get; set; }
    public string Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}
