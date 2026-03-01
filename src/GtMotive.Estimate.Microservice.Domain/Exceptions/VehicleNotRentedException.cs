namespace GtMotive.Estimate.Microservice.Domain.Exceptions
{
    /// <summary>
    /// Thrown when a return is requested for a non-rented vehicle.
    /// </summary>
    public sealed class VehicleNotRentedException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleNotRentedException"/> class.
        /// </summary>
        /// <param name="vehicleId">Vehicle identifier.</param>
        public VehicleNotRentedException(string vehicleId)
            : base($"Vehicle '{vehicleId}' is not rented.")
        {
        }
    }
}
