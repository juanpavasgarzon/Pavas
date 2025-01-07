using SharedKernel;

namespace Domain.MeasurementUnits;

public static class MeasurementUnitErrors
{
    public static readonly Error NotFound = Error.NotFound(
        "MeasurementUnit.NotFound",
        "The provided measurement was not found");

    public static readonly Error CanNotDelete = Error.Problem(
        "MeasurementUnit.CanNotDelete",
        "The provided measurement unit can't delete");

    public static readonly Error SymbolNotUnique = Error.Conflict(
        "MeasurementUnit.CodeNotUnique",
        "The provided measurement symbol is not unique");
}
