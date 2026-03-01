using System;

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
        /// <param name="manufactureDate">Invalid manufacture date.</param>
        public VehicleTooOldForFleetException(DateOnly manufactureDate)
            : base($"Vehicle manufacture date '{manufactureDate:yyyy-MM-dd}' is older than five years.")
        {
        }
    }
}
