using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.MeasurementUnits;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.MeasurementUnits.Delete;

internal sealed class DeleteMeasurementUnitCommandHandler(
    IApplicationDbContext context
) : ICommandHandler<DeleteMeasurementUnitCommand>
{
    public async Task<Result> Handle(DeleteMeasurementUnitCommand command, CancellationToken cancellationToken)
    {
        MeasurementUnit? measurementUnit = await context.MeasurementUnits
            .SingleOrDefaultAsync(m => m.Id == command.MeasurementUnitId, cancellationToken);

        if (measurementUnit is null)
        {
            return Result.Failure(MeasurementUnitErrors.NotFound);
        }

        try
        {
            context.MeasurementUnits.Remove(measurementUnit);

            await context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
        catch (DbUpdateException)
        {
            return Result.Failure(MeasurementUnitErrors.CanNotDelete);
        }
    }
}
