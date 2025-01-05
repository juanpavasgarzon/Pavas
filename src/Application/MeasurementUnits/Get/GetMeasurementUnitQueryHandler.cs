using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.MeasurementUnits.Get;

internal sealed class GetMeasurementUnitQueryHandler(
    IApplicationDbContext context
) : IQueryHandler<GetMeasurementUnitsQuery, List<MeasurementUnitResponse>>
{
    public async Task<Result<List<MeasurementUnitResponse>>> Handle(GetMeasurementUnitsQuery request,
        CancellationToken cancellationToken)
    {
        List<MeasurementUnitResponse> measurementUnits = await context.MeasurementUnits
            .AsNoTracking()
            .Select(measurementUnit => new MeasurementUnitResponse
            {
                Id = measurementUnit.Id,
                Name = measurementUnit.Name,
                Symbol = measurementUnit.Symbol,
                CreatedAt = measurementUnit.CreatedAt
            })
            .ToListAsync(cancellationToken);

        return measurementUnits;
    }
}
