using GtMotive.Estimate.Microservice.Api.ViewModels;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.ListVehicles;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicles
{
    public sealed class ListVehiclesPresenter : IWebApiPresenter, IListVehiclesOutputPort
    {
        public IActionResult ActionResult { get; private set; }

        public void StandardHandle(ListVehiclesOutput output)
        {
            ActionResult = new OkObjectResult(
                output.Vehicles
                    .Select(v => new VehicleModel(
                        v.VehicleId,
                        v.LicensePlate,
                        v.ManufactureDate,
                        v.Brand,
                        v.Model,
                        v.IsDeleted,
                        v.DeletedAtUtc))
                    .ToList());
        }
    }
}
