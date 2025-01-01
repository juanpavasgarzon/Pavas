using SharedKernel;

namespace Domain.Quotations;

public class Quotation : Entity
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public string SupplierId { get; set; }
    public Status Status { get; set; }
    public decimal TotalAmount { get; set; }
    public string Notes { get; set; }
    public DateTime CreatedAt { get; set; }

    public IEnumerable<QuotationItem> Items { get; set; } = [];
}
