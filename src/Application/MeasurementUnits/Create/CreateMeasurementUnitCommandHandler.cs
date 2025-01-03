using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.MeasurementUnits;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.MeasurementUnits.Create;

internal sealed class CreateMeasurementUnitCommandHandler(
    IApplicationDbContext context,
    IDateTimeProvider dateTimeProvider
) : ICommandHandler<CreateMeasurementUnitCommand, Guid>
{
    public async Task<Result<Guid>> Handle(CreateMeasurementUnitCommand command, CancellationToken cancellationToken)
    {
        if (await context.MeasurementUnits.AnyAsync(m => m.Symbol == command.Symbol, cancellationToken))
        {
            return Result.Failure<Guid>(MeasurementUnitErrors.SymbolNotUnique);
        }

        var measurementUnit = new MeasurementUnit
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Symbol = command.Symbol,
            CreatedAt = dateTimeProvider.UtcNow
        };

        context.MeasurementUnits.Add(measurementUnit);

        await context.SaveChangesAsync(cancellationToken);

        return measurementUnit.Id;
    }
}
