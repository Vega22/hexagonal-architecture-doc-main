using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.ListVehicles
{
    public interface IListVehiclesUseCase : IUseCase<ListVehiclesInput>
    {
    }

    public interface IListVehiclesOutputPort
    {
        void StandardHandle(ListVehiclesOutput output);
    }

    public sealed class ListVehiclesInput : IUseCaseInput
    {
        public ListVehiclesInput(bool includeDeleted, bool onlyOverFiveYears = false)
        {
            IncludeDeleted = includeDeleted;
            OnlyOverFiveYears = onlyOverFiveYears;
        }

        public bool IncludeDeleted { get; }

        public bool OnlyOverFiveYears { get; }
    }

    public sealed class VehicleListItemOutput
    {
        public VehicleListItemOutput(
            Guid vehicleId,
            string licensePlate,
            DateOnly manufactureDate,
            string brand,
            string model,
            bool isDeleted,
            DateTime? deletedAtUtc)
        {
            VehicleId = vehicleId;
            LicensePlate = licensePlate;
            ManufactureDate = manufactureDate;
            Brand = brand;
            Model = model;
            IsDeleted = isDeleted;
            DeletedAtUtc = deletedAtUtc;
        }

        public Guid VehicleId { get; }

        public string LicensePlate { get; }

        public DateOnly ManufactureDate { get; }

        public string Brand { get; }

        public string Model { get; }

        public bool IsDeleted { get; }

        public DateTime? DeletedAtUtc { get; }
    }

    public sealed class ListVehiclesOutput : IUseCaseOutput
    {
        public ListVehiclesOutput(IReadOnlyCollection<VehicleListItemOutput> vehicles)
        {
            Vehicles = vehicles;
        }

        public IReadOnlyCollection<VehicleListItemOutput> Vehicles { get; }
    }

    public sealed class ListVehiclesUseCase : IListVehiclesUseCase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IListVehiclesOutputPort _outputPort;

        public ListVehiclesUseCase(IVehicleRepository vehicleRepository, IListVehiclesOutputPort outputPort)
        {
            _vehicleRepository = vehicleRepository;
            _outputPort = outputPort;
        }

        public async Task Execute(ListVehiclesInput input)
        {
            var vehicles = await _vehicleRepository.List(input.IncludeDeleted);

            if (input.OnlyOverFiveYears)
            {
                var threshold = DateOnly.FromDateTime(DateTime.UtcNow).AddYears(-5);
                vehicles = vehicles
                    .Where(v => v.ManufactureDate.Value < threshold)
                    .ToList();
            }

            var output = new ListVehiclesOutput(
                vehicles
                    .Select(v => new VehicleListItemOutput(
                        v.Id.Value,
                        v.LicensePlate.Value,
                        v.ManufactureDate.Value,
                        v.Brand,
                        v.Model,
                        v.IsDeleted,
                        v.DeletedAtUtc))
                    .ToList());

            _outputPort.StandardHandle(output);
        }
    }
}
