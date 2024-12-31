using FluentValidation;

namespace Application.Suppliers.Delete;

internal sealed class DeleteSupplierCommandValidator : AbstractValidator<DeleteSupplierCommand>
{
    public DeleteSupplierCommandValidator()
    {
        RuleFor(c => c.SupplierId).NotEmpty();
    }
}
