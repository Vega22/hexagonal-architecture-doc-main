namespace GtMotive.Estimate.Microservice.Domain.Events
{
    /// <summary>
    /// Customer created.
    /// </summary>
    public sealed record CustomerCreatedDomainEvent(
        Guid CustomerId,
        string CountryCode,
        string DocumentType,
        string DocumentNumber) : DomainEventBase;

    /// <summary>
    /// Customer updated.
    /// </summary>
    public sealed record CustomerUpdatedDomainEvent(
        Guid CustomerId,
        string CountryCode,
        string DocumentType,
        string DocumentNumber) : DomainEventBase;

    /// <summary>
    /// Customer deleted.
    /// </summary>
    public sealed record CustomerDeletedDomainEvent(
        Guid CustomerId,
        DateTime DeletedAtUtc) : DomainEventBase;
}
