using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.MeasurementUnits;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.MeasurementUnits.GetById;

internal sealed class GetMeasurementUnitByIdQueryHandler(
    IApplicationDbContext context
) : IQueryHandler<GetMeasurementUnitByIdQuery, MeasurementUnitResponse>
{
    public async Task<Result<MeasurementUnitResponse>> Handle(GetMeasurementUnitByIdQuery query,
        CancellationToken cancellationToken)
    {
        MeasurementUnitResponse? measurementUnit = await context.MeasurementUnits
            .Where(m => m.Id == query.MeasurementUnitId)
            .Select(measurementUnit => new MeasurementUnitResponse
            {
                Id = measurementUnit.Id,
                Name = measurementUnit.Name,
                Symbol = measurementUnit.Symbol,
                CreatedAt = measurementUnit.CreatedAt
            })
            .SingleOrDefaultAsync(cancellationToken);

        return measurementUnit ?? Result.Failure<MeasurementUnitResponse>(MeasurementUnitErrors.NotFound);
    }
}
