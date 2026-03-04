using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// Encapsulates vehicle availability rules.
    /// </summary>
    public interface IVehicleAvailabilityService
    {
        /// <summary>
        /// Lists all currently available vehicles.
        /// </summary>
        /// <returns>Available vehicles.</returns>
        Task<IReadOnlyCollection<Vehicle>> ListAvailableVehicles();

        /// <summary>
        /// Returns true when vehicle can be rented.
        /// </summary>
        /// <param name="vehicleId">Vehicle id.</param>
        /// <returns>True when available.</returns>
        Task<bool> IsVehicleAvailable(VehicleId vehicleId);
    }
}
