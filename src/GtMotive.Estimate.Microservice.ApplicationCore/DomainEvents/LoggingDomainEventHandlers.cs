using GtMotive.Estimate.Microservice.Domain.Events;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.DomainEvents
{
    public sealed class VehicleCreatedDomainEventHandler : IDomainEventHandler<VehicleCreatedDomainEvent>
    {
        private readonly IAppLogger<VehicleCreatedDomainEventHandler> _logger;

        public VehicleCreatedDomainEventHandler(IAppLogger<VehicleCreatedDomainEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(VehicleCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Vehicle created: {VehicleId}, plate {LicensePlate}, {Brand} {Model}.",
                domainEvent.VehicleId,
                domainEvent.LicensePlate,
                domainEvent.Brand,
                domainEvent.Model);
            return Task.CompletedTask;
        }
    }

    public sealed class VehicleUpdatedDomainEventHandler : IDomainEventHandler<VehicleUpdatedDomainEvent>
    {
        private readonly IAppLogger<VehicleUpdatedDomainEventHandler> _logger;

        public VehicleUpdatedDomainEventHandler(IAppLogger<VehicleUpdatedDomainEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(VehicleUpdatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Vehicle updated: {VehicleId}, plate {LicensePlate}.", domainEvent.VehicleId, domainEvent.LicensePlate);
            return Task.CompletedTask;
        }
    }

    public sealed class VehicleDeletedDomainEventHandler : IDomainEventHandler<VehicleDeletedDomainEvent>
    {
        private readonly IAppLogger<VehicleDeletedDomainEventHandler> _logger;

        public VehicleDeletedDomainEventHandler(IAppLogger<VehicleDeletedDomainEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(VehicleDeletedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Vehicle deleted: {VehicleId} at {DeletedAtUtc}.", domainEvent.VehicleId, domainEvent.DeletedAtUtc);
            return Task.CompletedTask;
        }
    }

    public sealed class RentalStartedDomainEventHandler : IDomainEventHandler<RentalStartedDomainEvent>
    {
        private readonly IAppLogger<RentalStartedDomainEventHandler> _logger;

        public RentalStartedDomainEventHandler(IAppLogger<RentalStartedDomainEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(RentalStartedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Rental started: {RentalId}, vehicle {VehicleId}, customer {CustomerId}.",
                domainEvent.RentalId,
                domainEvent.VehicleId,
                domainEvent.CustomerId);
            return Task.CompletedTask;
        }
    }

    public sealed class RentalUpdatedDomainEventHandler : IDomainEventHandler<RentalUpdatedDomainEvent>
    {
        private readonly IAppLogger<RentalUpdatedDomainEventHandler> _logger;

        public RentalUpdatedDomainEventHandler(IAppLogger<RentalUpdatedDomainEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(RentalUpdatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Rental updated: {RentalId}, active {IsActive}, end {EndAtUtc}.",
                domainEvent.RentalId,
                domainEvent.IsActive,
                domainEvent.EndAtUtc);
            return Task.CompletedTask;
        }
    }

    public sealed class RentalReturnedDomainEventHandler : IDomainEventHandler<RentalReturnedDomainEvent>
    {
        private readonly IAppLogger<RentalReturnedDomainEventHandler> _logger;

        public RentalReturnedDomainEventHandler(IAppLogger<RentalReturnedDomainEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(RentalReturnedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Rental returned: {RentalId}, vehicle {VehicleId}, customer {CustomerId}.",
                domainEvent.RentalId,
                domainEvent.VehicleId,
                domainEvent.CustomerId);
            return Task.CompletedTask;
        }
    }

    public sealed class CustomerCreatedDomainEventHandler : IDomainEventHandler<CustomerCreatedDomainEvent>
    {
        private readonly IAppLogger<CustomerCreatedDomainEventHandler> _logger;

        public CustomerCreatedDomainEventHandler(IAppLogger<CustomerCreatedDomainEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(CustomerCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Customer created: {CustomerId}, {CountryCode}-{DocumentType}.",
                domainEvent.CustomerId,
                domainEvent.CountryCode,
                domainEvent.DocumentType);
            return Task.CompletedTask;
        }
    }

    public sealed class CustomerUpdatedDomainEventHandler : IDomainEventHandler<CustomerUpdatedDomainEvent>
    {
        private readonly IAppLogger<CustomerUpdatedDomainEventHandler> _logger;

        public CustomerUpdatedDomainEventHandler(IAppLogger<CustomerUpdatedDomainEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(CustomerUpdatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Customer updated: {CustomerId}.", domainEvent.CustomerId);
            return Task.CompletedTask;
        }
    }

    public sealed class CustomerDeletedDomainEventHandler : IDomainEventHandler<CustomerDeletedDomainEvent>
    {
        private readonly IAppLogger<CustomerDeletedDomainEventHandler> _logger;

        public CustomerDeletedDomainEventHandler(IAppLogger<CustomerDeletedDomainEventHandler> logger)
        {
            _logger = logger;
        }

        public Task Handle(CustomerDeletedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Customer deleted: {CustomerId}.", domainEvent.CustomerId);
            return Task.CompletedTask;
        }
    }
}
