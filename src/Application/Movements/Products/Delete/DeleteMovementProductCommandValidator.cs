using FluentValidation;

namespace Application.Movements.Products.Delete;

internal sealed class DeleteMovementProductCommandValidator : AbstractValidator<DeleteMovementProductCommand>
{
    public DeleteMovementProductCommandValidator()
    {
        RuleFor(c => c.MovementId)
            .NotEmpty();

        RuleFor(c => c.ProductId)
            .NotEmpty();
    }
}
