namespace Domain.Quotations;

public class QuotationItem
{
    public int Id { get; set; }
    public Guid QuotationId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public int Total { get; set; }
    public string Notes { get; set; }
}
