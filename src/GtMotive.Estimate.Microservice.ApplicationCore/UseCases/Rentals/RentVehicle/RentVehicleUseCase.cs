using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.RentVehicle
{
    /// <summary>
    /// Rent vehicle use case implementation.
    /// </summary>
    public sealed class RentVehicleUseCase : IRentVehicleUseCase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRentVehicleOutputPort _outputPort;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleUseCase"/> class.
        /// </summary>
        public RentVehicleUseCase(
            IVehicleRepository vehicleRepository,
            IRentalRepository rentalRepository,
            IUnitOfWork unitOfWork,
            IRentVehicleOutputPort outputPort)
        {
            _vehicleRepository = vehicleRepository;
            _rentalRepository = rentalRepository;
            _unitOfWork = unitOfWork;
            _outputPort = outputPort;
        }

        /// <inheritdoc/>
        public async Task Execute(RentVehicleInput input)
        {
            var vehicle = await _vehicleRepository.GetById(input.VehicleId);
            if (vehicle is null)
            {
                _outputPort.NotFoundHandle($"Vehicle '{input.VehicleId}' was not found.");
                return;
            }

            if (await _rentalRepository.HasActiveRental(input.CustomerId))
            {
                _outputPort.CustomerAlreadyHasActiveRentalHandle(
                    $"Customer '{input.CustomerId}' already has an active rental.");
                return;
            }

            if (await _rentalRepository.HasActiveRentalForVehicle(input.VehicleId))
            {
                _outputPort.VehicleUnavailableHandle($"Vehicle '{input.VehicleId}' is not available.");
                return;
            }

            var rentalStart = input.ReservedFromUtc.ToUniversalTime();
            await _rentalRepository.Add(Rental.Create(vehicle.Id.Value, input.CustomerId.Value, rentalStart));
            await _unitOfWork.Save();

            _outputPort.StandardHandle(new RentVehicleOutput(
                vehicle.Id.Value,
                input.CustomerId.Value,
                rentalStart));
        }
    }
}
