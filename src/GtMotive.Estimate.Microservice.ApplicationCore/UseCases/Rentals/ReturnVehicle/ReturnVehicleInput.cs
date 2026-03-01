using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.ReturnVehicle
{
    /// <summary>
    /// Input for return vehicle use case.
    /// </summary>
    public sealed class ReturnVehicleInput : IUseCaseInput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnVehicleInput"/> class.
        /// </summary>
        public ReturnVehicleInput(VehicleId vehicleId)
        {
            VehicleId = vehicleId;
        }

        /// <summary>
        /// Gets vehicle id.
        /// </summary>
        public VehicleId VehicleId { get; }
    }
}
