namespace GtMotive.Estimate.Microservice.Domain.Events
{
    /// <summary>
    /// Base record for domain events.
    /// </summary>
    public abstract record DomainEventBase : IDomainEvent
    {
        /// <inheritdoc />
        public DateTime OccurredOnUtc { get; init; } = DateTime.UtcNow;
    }
}
