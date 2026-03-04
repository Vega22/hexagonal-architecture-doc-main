using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.Events;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Rental aggregate for history and active reservations.
    /// </summary>
    public sealed class Rental : AggregateRoot
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rental"/> class.
        /// Required by EF.
        /// </summary>
        private Rental()
        {
        }

        private Rental(Guid id, Guid vehicleId, Guid customerId, DateTime startAtUtc)
        {
            Id = id;
            VehicleId = vehicleId;
            CustomerId = customerId;
            StartAtUtc = startAtUtc;
            EndAtUtc = null;
            IsActive = true;
        }

        /// <summary>
        /// Gets rental id.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets vehicle id.
        /// </summary>
        public Guid VehicleId { get; private set; }

        /// <summary>
        /// Gets customer id.
        /// </summary>
        public Guid CustomerId { get; private set; }

        /// <summary>
        /// Gets rental start date.
        /// </summary>
        public DateTime StartAtUtc { get; private set; }

        /// <summary>
        /// Gets rental end date, if returned.
        /// </summary>
        public DateTime? EndAtUtc { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the reservation is active.
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        /// Creates a new rental reservation.
        /// </summary>
        /// <param name="vehicleId">Vehicle id.</param>
        /// <param name="customerId">Customer id.</param>
        /// <param name="startAtUtc">Reservation start date.</param>
        /// <returns>Rental aggregate.</returns>
        public static Rental Create(Guid vehicleId, Guid customerId, DateTime startAtUtc)
        {
            if (vehicleId == Guid.Empty)
            {
                throw new DomainException("Vehicle id cannot be empty.");
            }

            if (customerId == Guid.Empty)
            {
                throw new DomainException("Customer id is required.");
            }

            ValidateSchedule(startAtUtc, null);
            var rental = new Rental(Guid.NewGuid(), vehicleId, customerId, startAtUtc);
            rental.AddDomainEvent(new RentalStartedDomainEvent(rental.Id, rental.VehicleId, rental.CustomerId, rental.StartAtUtc));
            return rental;
        }

        /// <summary>
        /// Updates rental values.
        /// </summary>
        /// <param name="vehicleId">Vehicle id.</param>
        /// <param name="customerId">Customer id.</param>
        /// <param name="startAtUtc">Start date in UTC.</param>
        /// <param name="endAtUtc">End date in UTC.</param>
        public void Update(Guid vehicleId, Guid customerId, DateTime startAtUtc, DateTime? endAtUtc)
        {
            if (vehicleId == Guid.Empty)
            {
                throw new DomainException("Vehicle id cannot be empty.");
            }

            if (customerId == Guid.Empty)
            {
                throw new DomainException("Customer id is required.");
            }

            ValidateSchedule(startAtUtc, endAtUtc);

            VehicleId = vehicleId;
            CustomerId = customerId;
            StartAtUtc = startAtUtc;
            EndAtUtc = endAtUtc;
            IsActive = endAtUtc is null;
            AddDomainEvent(new RentalUpdatedDomainEvent(Id, VehicleId, CustomerId, StartAtUtc, EndAtUtc, IsActive));
        }

        /// <summary>
        /// Closes a rental record.
        /// </summary>
        /// <param name="returnedAtUtc">Return date.</param>
        public void Complete(DateTime returnedAtUtc)
        {
            if (!IsActive)
            {
                throw new DomainException("Rental is already returned.");
            }

            if (returnedAtUtc == default)
            {
                throw new DomainException("Return date is required.");
            }

            if (returnedAtUtc < StartAtUtc)
            {
                throw new DomainException("Return date must be greater than or equal to start date.");
            }

            IsActive = false;
            EndAtUtc = returnedAtUtc;
            AddDomainEvent(new RentalReturnedDomainEvent(Id, VehicleId, CustomerId, returnedAtUtc));
        }

        private static void ValidateSchedule(DateTime startAtUtc, DateTime? endAtUtc)
        {
            if (startAtUtc == default)
            {
                throw new DomainException("Start date is required.");
            }

            if (endAtUtc.HasValue && endAtUtc.Value == default)
            {
                throw new DomainException("End date must be a valid date when provided.");
            }

            if (endAtUtc.HasValue && endAtUtc.Value < startAtUtc)
            {
                throw new DomainException("End date must be greater than or equal to start date.");
            }
        }
    }
}
