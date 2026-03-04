namespace GtMotive.Estimate.Microservice.Domain.Events
{
    /// <summary>
    /// Handles a specific domain event type.
    /// </summary>
    /// <typeparam name="TDomainEvent">Event type.</typeparam>
    public interface IDomainEventHandler<in TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        /// <summary>
        /// Handles the event asynchronously.
        /// </summary>
        /// <param name="domainEvent">Domain event payload.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>Task.</returns>
        Task Handle(TDomainEvent domainEvent, CancellationToken cancellationToken);
    }
}
