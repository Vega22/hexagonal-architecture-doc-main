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
        public VehicleUnavailableException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleUnavailableException"/> class.
        /// </summary>
        /// <param name="vehicleId">Vehicle identifier.</param>
        public VehicleUnavailableException(string vehicleId)
            : base($"Vehicle '{vehicleId}' is not available.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleUnavailableException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public VehicleUnavailableException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
