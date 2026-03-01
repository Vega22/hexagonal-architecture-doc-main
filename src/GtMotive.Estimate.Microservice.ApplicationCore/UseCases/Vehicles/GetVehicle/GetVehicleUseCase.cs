using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.GetVehicle
{
    public interface IGetVehicleUseCase : IUseCase<GetVehicleInput>
    {
    }

    public interface IGetVehicleOutputPort
    {
        void StandardHandle(GetVehicleOutput output);

        void NotFoundHandle(string message);
    }

    public sealed class GetVehicleInput : IUseCaseInput
    {
        public GetVehicleInput(VehicleId vehicleId)
        {
            VehicleId = vehicleId;
        }

        public VehicleId VehicleId { get; }
    }

    public sealed class GetVehicleOutput : IUseCaseOutput
    {
        public GetVehicleOutput(Guid vehicleId, string licensePlate, DateOnly manufactureDate, string brand, string model)
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

    public sealed class GetVehicleUseCase : IGetVehicleUseCase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IGetVehicleOutputPort _outputPort;

        public GetVehicleUseCase(IVehicleRepository vehicleRepository, IGetVehicleOutputPort outputPort)
        {
            _vehicleRepository = vehicleRepository;
            _outputPort = outputPort;
        }

        public async Task Execute(GetVehicleInput input)
        {
            var vehicle = await _vehicleRepository.GetById(input.VehicleId);
            if (vehicle is null)
            {
                _outputPort.NotFoundHandle($"Vehicle '{input.VehicleId}' was not found.");
                return;
            }

            _outputPort.StandardHandle(
                new GetVehicleOutput(
                    vehicle.Id.Value,
                    vehicle.LicensePlate.Value,
                    vehicle.ManufactureDate.Value,
                    vehicle.Brand,
                    vehicle.Model));
        }
    }
}
