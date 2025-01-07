using FluentValidation;

namespace Application.Movements.Products.Create;

internal sealed class CreateMovementProductCommandValidator : AbstractValidator<CreateMovementProductCommand>
{
    public CreateMovementProductCommandValidator()
    {
        RuleFor(c => c.MovementId)
            .NotEmpty();

        RuleFor(c => c.ProductId)
            .NotEmpty();

        RuleFor(c => c.Quantity)
            .NotEmpty()
            .GreaterThan(decimal.Zero);
    }
}
