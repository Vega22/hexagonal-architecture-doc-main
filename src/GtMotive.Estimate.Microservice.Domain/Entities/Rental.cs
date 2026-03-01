using GtMotive.Estimate.Microservice.Domain;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Rental aggregate for history and active reservations.
    /// </summary>
    public sealed class Rental
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rental"/> class.
        /// Required by EF.
        /// </summary>
        private Rental()
        {
        }

        private Rental(Guid id, Guid vehicleId, string customerId, DateTime startAtUtc)
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
        public string CustomerId { get; private set; }

        /// <summary>
        /// Gets rental start date.
        /// </summary>
        public DateTime StartAtUtc { get; private set; }

        /// <summary>
        /// Gets rental end date, if returned.
        /// </summary>
        public DateTime? EndAtUtc { get; private set; }

        /// <summary>
        /// Gets whether the reservation is active.
        /// </summary>
        public bool IsActive { get; private set; }

        /// <summary>
        /// Creates a new rental reservation.
        /// </summary>
        /// <param name="vehicleId">Vehicle id.</param>
        /// <param name="customerId">Customer id.</param>
        /// <param name="startAtUtc">Reservation start date.</param>
        /// <returns>Rental aggregate.</returns>
        public static Rental Create(Guid vehicleId, string customerId, DateTime startAtUtc)
        {
            ValidateSchedule(startAtUtc, null);
            return new Rental(Guid.NewGuid(), vehicleId, customerId, startAtUtc);
        }

        /// <summary>
        /// Updates rental values.
        /// </summary>
        /// <param name="vehicleId">Vehicle id.</param>
        /// <param name="customerId">Customer id.</param>
        /// <param name="startAtUtc">Start date in UTC.</param>
        /// <param name="endAtUtc">End date in UTC.</param>
        public void Update(Guid vehicleId, string customerId, DateTime startAtUtc, DateTime? endAtUtc)
        {
            if (vehicleId == Guid.Empty)
            {
                throw new DomainException("Vehicle id cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(customerId))
            {
                throw new DomainException("Customer id is required.");
            }

            ValidateSchedule(startAtUtc, endAtUtc);

            VehicleId = vehicleId;
            CustomerId = customerId.Trim();
            StartAtUtc = startAtUtc;
            EndAtUtc = endAtUtc;
            IsActive = endAtUtc is null;
        }

        /// <summary>
        /// Closes a rental record.
        /// </summary>
        /// <param name="returnedAtUtc">Return date.</param>
        public void Complete(DateTime returnedAtUtc)
        {
            IsActive = false;
            EndAtUtc = returnedAtUtc;
        }

        private static void ValidateSchedule(DateTime startAtUtc, DateTime? endAtUtc)
        {
            if (endAtUtc.HasValue && endAtUtc.Value < startAtUtc)
            {
                throw new DomainException("End date must be greater than or equal to start date.");
            }
        }
    }
}
