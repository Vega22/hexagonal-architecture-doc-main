using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.ListAvailableVehicles
{
    /// <summary>
    /// List available vehicles use case implementation.
    /// </summary>
    public sealed class ListAvailableVehiclesUseCase : IListAvailableVehiclesUseCase
    {
        private readonly IVehicleAvailabilityService _vehicleAvailabilityService;
        private readonly IListAvailableVehiclesOutputPort _outputPort;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListAvailableVehiclesUseCase"/> class.
        /// </summary>
        public ListAvailableVehiclesUseCase(
            IVehicleAvailabilityService vehicleAvailabilityService,
            IListAvailableVehiclesOutputPort outputPort)
        {
            _vehicleAvailabilityService = vehicleAvailabilityService;
            _outputPort = outputPort;
        }

        /// <inheritdoc/>
        public async Task Execute(ListAvailableVehiclesInput input)
        {
            var availableVehicles = await _vehicleAvailabilityService.ListAvailableVehicles();
            var items = availableVehicles
                .Select(vehicle => new AvailableVehicleItemOutput(
                    vehicle.Id.Value,
                    vehicle.LicensePlate.Value,
                    vehicle.ManufactureDate.Value,
                    vehicle.Brand,
                    vehicle.Model))
                .ToList();

            _outputPort.StandardHandle(new ListAvailableVehiclesOutput(items));
        }
    }
}
