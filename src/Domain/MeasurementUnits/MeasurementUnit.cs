using SharedKernel;

namespace Domain.MeasurementUnits;

public class MeasurementUnit : Entity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
}
