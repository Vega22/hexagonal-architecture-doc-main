using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.ListAvailableVehicles
{
    /// <summary>
    /// Output for list available vehicles use case.
    /// </summary>
    public sealed class ListAvailableVehiclesOutput : IUseCaseOutput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListAvailableVehiclesOutput"/> class.
        /// </summary>
        public ListAvailableVehiclesOutput(IReadOnlyCollection<AvailableVehicleItemOutput> vehicles)
        {
            Vehicles = vehicles;
        }

        /// <summary>
        /// Gets available vehicles.
        /// </summary>
        public IReadOnlyCollection<AvailableVehicleItemOutput> Vehicles { get; }
    }
}
