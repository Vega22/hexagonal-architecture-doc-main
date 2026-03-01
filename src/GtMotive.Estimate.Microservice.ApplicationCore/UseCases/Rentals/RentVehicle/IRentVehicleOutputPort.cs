using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.RentVehicle
{
    /// <summary>
    /// Output port for rent vehicle use case.
    /// </summary>
    public interface IRentVehicleOutputPort : IOutputPortStandard<RentVehicleOutput>, IOutputPortNotFound
    {
        /// <summary>
        /// Handles customer active rental conflict.
        /// </summary>
        /// <param name="message">Error message.</param>
        void CustomerAlreadyHasActiveRentalHandle(string message);

        /// <summary>
        /// Handles vehicle availability conflict.
        /// </summary>
        /// <param name="message">Error message.</param>
        void VehicleUnavailableHandle(string message);
    }
}
