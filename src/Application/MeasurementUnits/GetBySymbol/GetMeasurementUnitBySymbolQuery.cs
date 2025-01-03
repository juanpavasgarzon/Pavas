using Application.Abstractions.Messaging;

namespace Application.MeasurementUnits.GetBySymbol;

public sealed record GetMeasurementUnitBySymbolQuery(string Symbol) : IQuery<MeasurementUnitResponse>;
