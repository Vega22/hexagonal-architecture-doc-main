namespace GtMotive.Estimate.Microservice.Domain.Exceptions
{
    /// <summary>
    /// Thrown when a non-available vehicle is requested for rental.
    /// </summary>
    public sealed class VehicleUnavailableException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleUnavailableException"/> class.
        /// </summary>
        /// <param name="vehicleId">Vehicle identifier.</param>
        public VehicleUnavailableException(string vehicleId)
            : base($"Vehicle '{vehicleId}' is not available.")
        {
        }
    }
}
