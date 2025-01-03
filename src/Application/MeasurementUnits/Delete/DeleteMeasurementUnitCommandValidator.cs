using FluentValidation;

namespace Application.MeasurementUnits.Delete;

internal sealed class DeleteMeasurementUnitCommandValidator : AbstractValidator<DeleteMeasurementUnitCommand>
{
    public DeleteMeasurementUnitCommandValidator()
    {
        RuleFor(c => c.MeasurementUnitId).NotEmpty();
    }
}
