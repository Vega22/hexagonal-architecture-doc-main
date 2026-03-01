using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.RentVehicle
{
    /// <summary>
    /// Output for rent vehicle use case.
    /// </summary>
    public sealed class RentVehicleOutput : IUseCaseOutput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleOutput"/> class.
        /// </summary>
        public RentVehicleOutput(Guid vehicleId, string customerId, DateTime rentedAtUtc)
        {
            VehicleId = vehicleId;
            CustomerId = customerId;
            RentedAtUtc = rentedAtUtc;
        }

        /// <summary>
        /// Gets vehicle id.
        /// </summary>
        public Guid VehicleId { get; }

        /// <summary>
        /// Gets customer id.
        /// </summary>
        public string CustomerId { get; }

        /// <summary>
        /// Gets rented timestamp.
        /// </summary>
        public DateTime RentedAtUtc { get; }
    }
}
