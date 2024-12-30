using Application.Abstractions.Messaging;

namespace Application.Suppliers.Create;

public class CreateSupplierCommand : ICommand
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Notes { get; set; }
}
