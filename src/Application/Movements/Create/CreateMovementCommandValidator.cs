using FluentValidation;

namespace Application.Movements.Create;

internal sealed class CreateMovementCommandValidator : AbstractValidator<CreateMovementCommand>
{
    public CreateMovementCommandValidator()
    {
        RuleFor(c => c.Reference).NotEmpty();
        RuleFor(c => c.Type).NotEmpty().IsInEnum();
    }
}
