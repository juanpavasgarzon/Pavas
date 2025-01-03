using Application.Abstractions.Messaging;

namespace Application.MeasurementUnits.Create;

public sealed record CreateMeasurementUnitCommand(string Name, string Symbol) : ICommand<Guid>;
