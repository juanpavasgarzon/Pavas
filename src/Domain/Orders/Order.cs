namespace Domain.Orders;

public class Order
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public string CustomerId { get; set; }
    public Status Status { get; set; }
    public decimal TotalAmount { get; set; }
    public string Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}
