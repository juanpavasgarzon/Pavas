using Application.Abstractions.Messaging;

namespace Application.MeasurementUnits.GetById;

public sealed record GetMeasurementUnitByIdQuery(Guid MeasurementUnitId) : IQuery<MeasurementUnitResponse>;
