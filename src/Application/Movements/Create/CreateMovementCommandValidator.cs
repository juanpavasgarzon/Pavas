using Domain.Movements;
using FluentValidation;

namespace Application.Movements.Create;

internal sealed class CreateMovementCommandValidator : AbstractValidator<CreateMovementCommand>
{
    public CreateMovementCommandValidator()
    {
        RuleFor(c => c.Reference)
            .NotEmpty();

        RuleFor(c => c.Type)
            .NotEmpty()
            .Must(type => Enum.TryParse(type, true, out MovementType result) && Enum.IsDefined(result))
            .WithMessage("Type must be one of the following values: In, Out, Transfer.");
    }
}
