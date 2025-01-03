using Application.Abstractions.Messaging;

namespace Application.MeasurementUnits.Delete;

public sealed record DeleteMeasurementUnitCommand(Guid MeasurementUnitId) : ICommand;
