using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Services
{
    /// <summary>
    /// Service that applies vehicle availability business rules.
    /// </summary>
    public sealed class VehicleAvailabilityService : IVehicleAvailabilityService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IRentalRepository _rentalRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleAvailabilityService"/> class.
        /// </summary>
        /// <param name="vehicleRepository">Vehicle repository.</param>
        /// <param name="rentalRepository">Rental repository.</param>
        public VehicleAvailabilityService(
            IVehicleRepository vehicleRepository,
            IRentalRepository rentalRepository)
        {
            _vehicleRepository = vehicleRepository;
            _rentalRepository = rentalRepository;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<Vehicle>> ListAvailableVehicles()
        {
            var vehicles = await _vehicleRepository.List(includeDeleted: false);
            var availableVehicles = new List<Vehicle>(vehicles.Count);

            foreach (var vehicle in vehicles)
            {
                var hasActiveRental = await _rentalRepository.HasActiveRentalForVehicle(vehicle.Id);
                if (!hasActiveRental)
                {
                    availableVehicles.Add(vehicle);
                }
            }

            return availableVehicles;
        }

        /// <inheritdoc />
        public async Task<bool> IsVehicleAvailable(VehicleId vehicleId)
        {
            return !await _rentalRepository.HasActiveRentalForVehicle(vehicleId);
        }
    }
}
