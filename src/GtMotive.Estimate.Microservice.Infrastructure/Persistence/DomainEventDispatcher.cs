#nullable enable
using GtMotive.Estimate.Microservice.Domain.Events;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GtMotive.Estimate.Microservice.Infrastructure.Persistence
{
    /// <summary>
    /// Resolves and invokes domain event handlers from DI.
    /// </summary>
    public sealed class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IAppLogger<DomainEventDispatcher> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEventDispatcher"/> class.
        /// </summary>
        /// <param name="serviceProvider">Service provider.</param>
        /// <param name="logger">Logger.</param>
        public DomainEventDispatcher(IServiceProvider serviceProvider, IAppLogger<DomainEventDispatcher> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task Dispatch(IReadOnlyCollection<IDomainEvent> domainEvents, CancellationToken cancellationToken = default)
        {
            foreach (var domainEvent in domainEvents)
            {
                var handlerInterfaceType = typeof(IDomainEventHandler<>).MakeGenericType(domainEvent.GetType());
                var handlers = _serviceProvider.GetServices(handlerInterfaceType);
                var handled = false;

                foreach (var handler in handlers)
                {
                    var handleMethod = handlerInterfaceType.GetMethod(nameof(IDomainEventHandler<IDomainEvent>.Handle));
                    if (handleMethod is null)
                    {
                        continue;
                    }

                    handled = true;
                    var task = (Task?)handleMethod.Invoke(handler, [domainEvent, cancellationToken]);
                    if (task is not null)
                    {
                        await task;
                    }
                }

                if (!handled)
                {
                    _logger.LogDebug("No domain event handlers registered for {EventType}.", domainEvent.GetType().Name);
                }
            }
        }
    }
}
