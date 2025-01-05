using FluentValidation;

namespace Application.Movements.Complete;

internal sealed class CompleteMovementCommandValidator : AbstractValidator<CompleteMovementCommand>
{
    public CompleteMovementCommandValidator()
    {
        RuleFor(c => c.MovementId).NotEmpty();
    }
}
