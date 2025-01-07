using FluentValidation;

namespace Application.MeasurementUnits.Create;

internal sealed class CreateMeasurementUnitCommandValidator : AbstractValidator<CreateMeasurementUnitCommand>
{
    public CreateMeasurementUnitCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty();

        RuleFor(c => c.Symbol)
            .NotEmpty()
            .MaximumLength(2);
    }
}
