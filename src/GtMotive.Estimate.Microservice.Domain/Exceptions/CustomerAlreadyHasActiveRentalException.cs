namespace GtMotive.Estimate.Microservice.Domain.Exceptions
{
    /// <summary>
    /// Thrown when a customer tries to rent a second vehicle.
    /// </summary>
    public sealed class CustomerAlreadyHasActiveRentalException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerAlreadyHasActiveRentalException"/> class.
        /// </summary>
        /// <param name="customerId">Customer identifier.</param>
        public CustomerAlreadyHasActiveRentalException(string customerId)
            : base($"Customer '{customerId}' already has an active rental.")
        {
        }
    }
}
