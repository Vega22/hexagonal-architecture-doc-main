namespace GtMotive.Estimate.Microservice.Domain.Exceptions
{
    /// <summary>
    /// Thrown when a vehicle manufacture date violates fleet age policy.
    /// </summary>
    public sealed class VehicleTooOldForFleetException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleTooOldForFleetException"/> class.
        /// </summary>
        public VehicleTooOldForFleetException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleTooOldForFleetException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public VehicleTooOldForFleetException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleTooOldForFleetException"/> class.
        /// </summary>
        /// <param name="manufactureDate">Invalid manufacture date.</param>
        public VehicleTooOldForFleetException(DateOnly manufactureDate)
            : base($"Vehicle manufacture date '{manufactureDate:yyyy-MM-dd}' is older than five years.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleTooOldForFleetException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public VehicleTooOldForFleetException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
