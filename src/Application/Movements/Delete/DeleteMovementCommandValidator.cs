using FluentValidation;

namespace Application.Movements.Delete;

internal sealed class DeleteMovementCommandValidator : AbstractValidator<DeleteMovementCommand>
{
    public DeleteMovementCommandValidator()
    {
        RuleFor(c => c.MovementId)
            .NotEmpty();
    }
}
