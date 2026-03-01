namespace GtMotive.Estimate.Microservice.Domain.Exceptions
{
    /// <summary>
    /// Thrown when a vehicle cannot be found.
    /// </summary>
    public sealed class VehicleNotFoundException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleNotFoundException"/> class.
        /// </summary>
        /// <param name="vehicleId">Vehicle identifier.</param>
        public VehicleNotFoundException(string vehicleId)
            : base($"Vehicle '{vehicleId}' was not found.")
        {
        }
    }
}
