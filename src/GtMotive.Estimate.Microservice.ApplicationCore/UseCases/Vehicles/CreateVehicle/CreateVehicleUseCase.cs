using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.CreateVehicle
{
    /// <summary>
    /// Create vehicle use case implementation.
    /// </summary>
    public sealed class CreateVehicleUseCase : ICreateVehicleUseCase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICreateVehicleOutputPort _outputPort;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVehicleUseCase"/> class.
        /// </summary>
        public CreateVehicleUseCase(
            IVehicleRepository vehicleRepository,
            IUnitOfWork unitOfWork,
            ICreateVehicleOutputPort outputPort)
        {
            _vehicleRepository = vehicleRepository;
            _unitOfWork = unitOfWork;
            _outputPort = outputPort;
        }

        /// <inheritdoc/>
        public async Task Execute(CreateVehicleInput input)
        {
            if (await _vehicleRepository.ExistsByLicensePlate(input.LicensePlate))
            {
                _outputPort.DuplicatedLicensePlateHandle($"License plate '{input.LicensePlate}' already exists.");
                return;
            }

            var vehicle = Vehicle.Create(input.LicensePlate, input.ManufactureDate, input.Brand, input.Model);
            await _vehicleRepository.Add(vehicle);
            await _unitOfWork.Save();

            var output = new CreateVehicleOutput(
                vehicle.Id.Value,
                vehicle.LicensePlate.Value,
                vehicle.ManufactureDate.Value,
                vehicle.Brand,
                vehicle.Model);

            _outputPort.StandardHandle(output);
        }
    }
}
