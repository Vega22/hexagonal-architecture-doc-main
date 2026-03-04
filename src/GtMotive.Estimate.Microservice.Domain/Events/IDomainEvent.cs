namespace GtMotive.Estimate.Microservice.Domain.Events
{
    /// <summary>
    /// Represents a domain event raised by an aggregate.
    /// </summary>
    public interface IDomainEvent
    {
        /// <summary>
        /// Gets event occurrence timestamp in UTC.
        /// </summary>
        DateTime OccurredOnUtc { get; }
    }
}
