using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.ListAvailableVehicles
{
    /// <summary>
    /// Output port for list available vehicles use case.
    /// </summary>
    public interface IListAvailableVehiclesOutputPort : IOutputPortStandard<ListAvailableVehiclesOutput>
    {
    }
}
