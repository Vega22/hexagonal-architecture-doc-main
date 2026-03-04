namespace GtMotive.Estimate.Microservice.Domain.Events
{
    /// <summary>
    /// Rental started.
    /// </summary>
    public sealed record RentalStartedDomainEvent(
        Guid RentalId,
        Guid VehicleId,
        Guid CustomerId,
        DateTime StartAtUtc) : DomainEventBase;

    /// <summary>
    /// Rental updated.
    /// </summary>
    public sealed record RentalUpdatedDomainEvent(
        Guid RentalId,
        Guid VehicleId,
        Guid CustomerId,
        DateTime StartAtUtc,
        DateTime? EndAtUtc,
        bool IsActive) : DomainEventBase;

    /// <summary>
    /// Rental closed/returned.
    /// </summary>
    public sealed record RentalReturnedDomainEvent(
        Guid RentalId,
        Guid VehicleId,
        Guid CustomerId,
        DateTime EndAtUtc) : DomainEventBase;
}
