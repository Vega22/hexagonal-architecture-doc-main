using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.CreateVehicle
{
    /// <summary>
    /// Output port for create vehicle use case.
    /// </summary>
    public interface ICreateVehicleOutputPort : IOutputPortStandard<CreateVehicleOutput>
    {
        /// <summary>
        /// Handles duplicated plate conflicts.
        /// </summary>
        /// <param name="message">Conflict detail.</param>
        void DuplicatedLicensePlateHandle(string message);
    }
}
