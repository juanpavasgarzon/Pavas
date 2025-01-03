using Application.Abstractions.Messaging;

namespace Application.MeasurementUnits.Get;

public sealed record GetMeasurementUnitsQuery : IQuery<List<MeasurementUnitResponse>>;
