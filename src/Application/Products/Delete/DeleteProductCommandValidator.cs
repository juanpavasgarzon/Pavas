using FluentValidation;

namespace Application.Products.Delete;

internal class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(c => c.ProductId).NotEmpty();
    }
}
