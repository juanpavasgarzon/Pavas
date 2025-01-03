namespace Application.MeasurementUnits.GetById;

public sealed class MeasurementUnitResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public DateTime CreatedAt { get; set; }
}
