using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.UpdateVehicle
{
    public interface IUpdateVehicleUseCase : IUseCase<UpdateVehicleInput>
    {
    }

    public interface IUpdateVehicleOutputPort
    {
        void StandardHandle(UpdateVehicleOutput output);

        void NotFoundHandle(string message);

        void DuplicatedLicensePlateHandle(string message);
    }

    public sealed class UpdateVehicleInput : IUseCaseInput
    {
        public UpdateVehicleInput(
            VehicleId vehicleId,
            LicensePlate licensePlate,
            ManufactureDate manufactureDate,
            string brand,
            string model)
        {
            VehicleId = vehicleId;
            LicensePlate = licensePlate;
            ManufactureDate = manufactureDate;
            Brand = brand;
            Model = model;
        }

        public VehicleId VehicleId { get; }

        public LicensePlate LicensePlate { get; }

        public ManufactureDate ManufactureDate { get; }

        public string Brand { get; }

        public string Model { get; }
    }

    public sealed class UpdateVehicleOutput : IUseCaseOutput
    {
        public UpdateVehicleOutput(Guid vehicleId, string licensePlate, DateOnly manufactureDate, string brand, string model)
        {
            VehicleId = vehicleId;
            LicensePlate = licensePlate;
            ManufactureDate = manufactureDate;
            Brand = brand;
            Model = model;
        }

        public Guid VehicleId { get; }

        public string LicensePlate { get; }

        public DateOnly ManufactureDate { get; }

        public string Brand { get; }

        public string Model { get; }
    }

    public sealed class UpdateVehicleUseCase : IUpdateVehicleUseCase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUpdateVehicleOutputPort _outputPort;

        public UpdateVehicleUseCase(
            IVehicleRepository vehicleRepository,
            IUnitOfWork unitOfWork,
            IUpdateVehicleOutputPort outputPort)
        {
            _vehicleRepository = vehicleRepository;
            _unitOfWork = unitOfWork;
            _outputPort = outputPort;
        }

        public async Task Execute(UpdateVehicleInput input)
        {
            var vehicle = await _vehicleRepository.GetById(input.VehicleId);
            if (vehicle is null)
            {
                _outputPort.NotFoundHandle($"Vehicle '{input.VehicleId}' was not found.");
                return;
            }

            if (await _vehicleRepository.ExistsByLicensePlate(input.LicensePlate, input.VehicleId.Value))
            {
                _outputPort.DuplicatedLicensePlateHandle($"License plate '{input.LicensePlate}' already exists.");
                return;
            }

            vehicle.UpdateDetails(input.LicensePlate, input.ManufactureDate, input.Brand, input.Model);
            await _unitOfWork.Save();

            _outputPort.StandardHandle(
                new UpdateVehicleOutput(
                    vehicle.Id.Value,
                    vehicle.LicensePlate.Value,
                    vehicle.ManufactureDate.Value,
                    vehicle.Brand,
                    vehicle.Model));
        }
    }
}
