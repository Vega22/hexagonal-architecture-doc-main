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
        private readonly IVehicleAvailabilityService _vehicleAvailabilityService;
        private readonly ICustomerRepository _customerRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRentVehicleOutputPort _outputPort;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleUseCase"/> class.
        /// </summary>
        public RentVehicleUseCase(
            IVehicleRepository vehicleRepository,
            IVehicleAvailabilityService vehicleAvailabilityService,
            ICustomerRepository customerRepository,
            IRentalRepository rentalRepository,
            IUnitOfWork unitOfWork,
            IRentVehicleOutputPort outputPort)
        {
            _vehicleRepository = vehicleRepository;
            _vehicleAvailabilityService = vehicleAvailabilityService;
            _customerRepository = customerRepository;
            _rentalRepository = rentalRepository;
            _unitOfWork = unitOfWork;
            _outputPort = outputPort;
        }

        /// <inheritdoc/>
        public async Task Execute(RentVehicleInput input)
        {
            await _unitOfWork.BeginTransaction();

            try
            {
                var vehicle = await _vehicleRepository.GetById(input.VehicleId);
                if (vehicle is null)
                {
                    await _unitOfWork.RollbackTransaction();
                    _outputPort.NotFoundHandle($"Vehicle '{input.VehicleId}' was not found.");
                    return;
                }

                var customer = await _customerRepository.GetById(input.CustomerId.Value);
                if (customer is null)
                {
                    await _unitOfWork.RollbackTransaction();
                    _outputPort.NotFoundHandle($"Customer '{input.CustomerId}' was not found.");
                    return;
                }

                if (await _rentalRepository.HasActiveRental(input.CustomerId))
                {
                    await _unitOfWork.RollbackTransaction();
                    _outputPort.CustomerAlreadyHasActiveRentalHandle(
                        $"Customer '{input.CustomerId}' already has an active rental.");
                    return;
                }

                var isAvailable = await _vehicleAvailabilityService.IsVehicleAvailable(input.VehicleId);
                if (!isAvailable)
                {
                    await _unitOfWork.RollbackTransaction();
                    _outputPort.VehicleUnavailableHandle($"Vehicle '{input.VehicleId}' is not available.");
                    return;
                }

                var rentalStart = input.ReservedFromUtc.ToUniversalTime();
                await _rentalRepository.Add(Rental.Create(vehicle.Id.Value, customer.Id, rentalStart));
                await _unitOfWork.Save();
                await _unitOfWork.CommitTransaction();

                _outputPort.StandardHandle(new RentVehicleOutput(
                    vehicle.Id.Value,
                    customer.Id,
                    rentalStart));
            }
            catch
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }
    }
}
