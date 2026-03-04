#nullable enable
using System.Collections.Generic;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// Vehicle repository contract.
    /// </summary>
    public interface IVehicleRepository
    {
        /// <summary>
        /// Adds a new vehicle.
        /// </summary>
        /// <param name="vehicle">Vehicle aggregate.</param>
        /// <returns>Task.</returns>
        Task Add(Vehicle vehicle);

        /// <summary>
        /// Gets a vehicle by id.
        /// </summary>
        /// <param name="vehicleId">Vehicle id.</param>
        /// <returns>Vehicle or null.</returns>
        Task<Vehicle?> GetById(VehicleId vehicleId);

        /// <summary>
        /// Gets a vehicle by id including soft deleted records.
        /// </summary>
        /// <param name="vehicleId">Vehicle id.</param>
        /// <returns>Vehicle or null.</returns>
        Task<Vehicle?> GetByIdIncludingDeleted(VehicleId vehicleId);

        /// <summary>
        /// Lists vehicles.
        /// </summary>
        /// <param name="includeDeleted">Whether soft-deleted records must be included.</param>
        /// <returns>Vehicles.</returns>
        Task<IReadOnlyCollection<Vehicle>> List(bool includeDeleted);

        /// <summary>
        /// Indicates whether plate already exists.
        /// </summary>
        /// <param name="licensePlate">License plate to check.</param>
        /// <param name="excludedVehicleId">Optional current vehicle id in update flows.</param>
        /// <returns>True when duplicated plate exists.</returns>
        Task<bool> ExistsByLicensePlate(LicensePlate licensePlate, Guid? excludedVehicleId = null);
    }
}
