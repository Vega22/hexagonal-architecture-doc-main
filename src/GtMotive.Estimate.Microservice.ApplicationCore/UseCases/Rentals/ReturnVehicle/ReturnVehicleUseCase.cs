using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.ReturnVehicle
{
    /// <summary>
    /// Return vehicle use case implementation.
    /// </summary>
    public sealed class ReturnVehicleUseCase : IReturnVehicleUseCase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReturnVehicleOutputPort _outputPort;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnVehicleUseCase"/> class.
        /// </summary>
        public ReturnVehicleUseCase(
            IVehicleRepository vehicleRepository,
            IRentalRepository rentalRepository,
            IUnitOfWork unitOfWork,
            IReturnVehicleOutputPort outputPort)
        {
            _vehicleRepository = vehicleRepository;
            _rentalRepository = rentalRepository;
            _unitOfWork = unitOfWork;
            _outputPort = outputPort;
        }

        /// <inheritdoc/>
        public async Task Execute(ReturnVehicleInput input)
        {
            var vehicle = await _vehicleRepository.GetById(input.VehicleId);
            if (vehicle is null)
            {
                _outputPort.NotFoundHandle($"Vehicle '{input.VehicleId}' was not found.");
                return;
            }

            var activeRental = await _rentalRepository.GetActiveByVehicle(input.VehicleId);
            if (activeRental is null)
            {
                _outputPort.VehicleNotRentedHandle($"Vehicle '{input.VehicleId}' is not rented.");
                return;
            }

            activeRental.Complete(DateTime.UtcNow);
            await _unitOfWork.Save();

            _outputPort.StandardHandle(new ReturnVehicleOutput(vehicle.Id.Value));
        }
    }
}
