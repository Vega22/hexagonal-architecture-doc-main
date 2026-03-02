#nullable enable
using GtMotive.Estimate.Microservice.Domain.Entities;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// Customer repository contract.
    /// </summary>
    public interface ICustomerRepository
    {
        /// <summary>
        /// Adds a customer.
        /// </summary>
        Task Add(Customer customer);

        /// <summary>
        /// Gets customer by id excluding soft deleted.
        /// </summary>
        Task<Customer?> GetById(Guid customerId);

        /// <summary>
        /// Gets customer by id including soft deleted.
        /// </summary>
        Task<Customer?> GetByIdIncludingDeleted(Guid customerId);

        /// <summary>
        /// Lists customers.
        /// </summary>
        Task<IReadOnlyCollection<Customer>> List(bool includeDeleted);

        /// <summary>
        /// Indicates whether document is duplicated.
        /// </summary>
        Task<bool> ExistsByDocument(
            string countryCode,
            string documentType,
            string documentNumber,
            Guid? excludedCustomerId = null);

        /// <summary>
        /// Indicates whether email is duplicated.
        /// </summary>
        Task<bool> ExistsByEmail(string email, Guid? excludedCustomerId = null);
    }
}
