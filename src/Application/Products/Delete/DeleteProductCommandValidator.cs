using FluentValidation;

namespace Application.Products.Delete;

internal sealed class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(c => c.ProductId)
            .NotEmpty();
    }
}
