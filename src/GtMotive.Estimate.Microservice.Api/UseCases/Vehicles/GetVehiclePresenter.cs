using GtMotive.Estimate.Microservice.Api.ViewModels;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.GetVehicle;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicles
{
    public sealed class GetVehiclePresenter : IWebApiPresenter, IGetVehicleOutputPort
    {
        public IActionResult ActionResult { get; private set; }

        public void StandardHandle(GetVehicleOutput output)
        {
            ActionResult = new OkObjectResult(
                new VehicleModel(
                    output.VehicleId,
                    output.LicensePlate,
                    output.ManufactureDate,
                    output.Brand,
                    output.Model,
                    false,
                    null));
        }

        public void NotFoundHandle(string message)
        {
            ActionResult = new NotFoundObjectResult(message);
        }
    }
}
