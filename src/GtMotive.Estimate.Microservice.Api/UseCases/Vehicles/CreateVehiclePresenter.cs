using GtMotive.Estimate.Microservice.Api.ViewModels;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.CreateVehicle;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicles
{
    /// <summary>
    /// Presenter for create vehicle use case.
    /// </summary>
    public sealed class CreateVehiclePresenter : IWebApiPresenter, ICreateVehicleOutputPort
    {
        /// <inheritdoc/>
        public IActionResult ActionResult { get; private set; }

        /// <inheritdoc/>
        public void StandardHandle(CreateVehicleOutput response)
        {
            var body = new CreateVehicleResponseModel(
                response.VehicleId,
                response.LicensePlate,
                response.ManufactureDate,
                response.Brand,
                response.Model);
            ActionResult = new CreatedResult($"/api/vehicles/{response.VehicleId}", body);
        }

        /// <inheritdoc/>
        public void DuplicatedLicensePlateHandle(string message)
        {
            ActionResult = new ConflictObjectResult(message);
        }
    }
}
