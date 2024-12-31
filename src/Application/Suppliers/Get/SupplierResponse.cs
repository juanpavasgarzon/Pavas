namespace Application.Suppliers.Get;

public sealed class SupplierResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Notes { get; set; }
    public DateTime CreatedAt { get; set; }   
}
