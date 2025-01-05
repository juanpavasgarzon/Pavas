using Domain.Products;
using SharedKernel;

namespace Domain.Suppliers;

public class Supplier : Entity
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Notes { get; set; }
    public DateTime CreatedAt { get; set; }
    public ICollection<Product> Products { get; set; }
}
