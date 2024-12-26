namespace Domain.Invoices;

public class Invoice
{
    public Guid Id { get; set; }
    public Guid QuotationId { get; set; }
    public Status Status { get; set; }
    public decimal TotalAmount { get; set; }
    public string Notes { get; set; }
    public DateTime CreatedAt { get; set; }
}
