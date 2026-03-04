namespace GtMotive.Estimate.Microservice.Domain.Events
{
    /// <summary>
    /// Vehicle was created.
    /// </summary>
    public sealed record VehicleCreatedDomainEvent(
        Guid VehicleId,
        string LicensePlate,
        DateOnly ManufactureDate,
        string Brand,
        string Model) : DomainEventBase;

    /// <summary>
    /// Vehicle details were updated.
    /// </summary>
    public sealed record VehicleUpdatedDomainEvent(
        Guid VehicleId,
        string LicensePlate,
        DateOnly ManufactureDate,
        string Brand,
        string Model) : DomainEventBase;

    /// <summary>
    /// Vehicle was soft deleted.
    /// </summary>
    public sealed record VehicleDeletedDomainEvent(
        Guid VehicleId,
        DateTime DeletedAtUtc) : DomainEventBase;
}
