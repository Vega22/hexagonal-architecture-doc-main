using GtMotive.Estimate.Microservice.Api.ViewModels;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.ListAvailableVehicles;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicles
{
    /// <summary>
    /// Presenter for listing available vehicles.
    /// </summary>
    public sealed class ListAvailableVehiclesPresenter : IWebApiPresenter, IListAvailableVehiclesOutputPort
    {
        /// <inheritdoc/>
        public IActionResult ActionResult { get; private set; }

        /// <inheritdoc/>
        public void StandardHandle(ListAvailableVehiclesOutput response)
        {
            var body = response.Vehicles
                .Select(item => new AvailableVehicleModel(
                    item.VehicleId,
                    item.LicensePlate,
                    item.ManufactureDate,
                    item.Brand,
                    item.Model))
                .ToList();

            ActionResult = new OkObjectResult(body);
        }
    }
}
