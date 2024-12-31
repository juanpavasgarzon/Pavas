using FluentValidation;

namespace Application.Customers.Delete;

internal sealed class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerCommandValidator()
    {
        RuleFor(c => c.CustomerId).NotEmpty();
    }
}
