using Application.Abstractions.Messaging;

namespace Application.Customers.Create;

public class CreateCustomerCommand : ICommand
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string Notes { get; set; }
}
