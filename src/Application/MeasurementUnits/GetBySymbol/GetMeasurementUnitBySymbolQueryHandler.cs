using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.MeasurementUnits;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Application.MeasurementUnits.GetBySymbol;

internal sealed class GetMeasurementUnitBySymbolQueryHandler(
    IApplicationDbContext context
) : IQueryHandler<GetMeasurementUnitBySymbolQuery, MeasurementUnitResponse>
{
    public async Task<Result<MeasurementUnitResponse>> Handle(GetMeasurementUnitBySymbolQuery query,
        CancellationToken cancellationToken)
    {
        MeasurementUnitResponse? measurementUnit = await context.MeasurementUnits
            .AsNoTracking()
            .Where(m => m.Symbol == query.Symbol)
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
