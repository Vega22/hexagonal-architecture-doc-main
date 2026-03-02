#nullable enable
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// Repository contract to query rental constraints.
    /// </summary>
    public interface IRentalRepository
    {
        /// <summary>
        /// Indicates whether customer has an active rental.
        /// </summary>
        /// <param name="customerId">Customer id.</param>
        /// <returns>True if active rental exists.</returns>
        Task<bool> HasActiveRental(CustomerId customerId);

        /// <summary>
        /// Indicates whether a vehicle has an active rental.
        /// </summary>
        /// <param name="vehicleId">Vehicle id.</param>
        /// <returns>True if active rental exists for the vehicle.</returns>
        Task<bool> HasActiveRentalForVehicle(VehicleId vehicleId);

        /// <summary>
        /// Indicates whether customer has another active rental excluding one record.
        /// </summary>
        /// <param name="customerId">Customer id.</param>
        /// <param name="rentalId">Rental id to exclude.</param>
        /// <returns>True if active rental exists.</returns>
        Task<bool> HasActiveRentalExcluding(CustomerId customerId, Guid rentalId);

        /// <summary>
        /// Indicates whether vehicle has another active rental excluding one record.
        /// </summary>
        /// <param name="vehicleId">Vehicle id.</param>
        /// <param name="rentalId">Rental id to exclude.</param>
        /// <returns>True if active rental exists.</returns>
        Task<bool> HasActiveRentalForVehicleExcluding(VehicleId vehicleId, Guid rentalId);

        /// <summary>
        /// Adds a rental record.
        /// </summary>
        /// <param name="rental">Rental aggregate.</param>
        /// <returns>Task.</returns>
        Task Add(Rental rental);

        /// <summary>
        /// Gets active rental by vehicle id.
        /// </summary>
        /// <param name="vehicleId">Vehicle id.</param>
        /// <returns>Rental record or null.</returns>
        Task<Rental?> GetActiveByVehicle(VehicleId vehicleId);

        /// <summary>
        /// Gets rental by id.
        /// </summary>
        /// <param name="rentalId">Rental id.</param>
        /// <returns>Rental or null.</returns>
        Task<Rental?> GetById(Guid rentalId);

        /// <summary>
        /// Lists rentals.
        /// </summary>
        /// <returns>Rentals.</returns>
        Task<IReadOnlyCollection<Rental>> List();

        /// <summary>
        /// Removes a rental.
        /// </summary>
        /// <param name="rental">Rental aggregate.</param>
        /// <returns>Task.</returns>
        Task Remove(Rental rental);
    }
}
