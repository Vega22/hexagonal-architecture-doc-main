using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.DeleteVehicle;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicles
{
    public sealed class DeleteVehiclePresenter : IWebApiPresenter, IDeleteVehicleOutputPort
    {
        public IActionResult ActionResult { get; private set; }

        public void StandardHandle()
        {
            ActionResult = new NoContentResult();
        }

        public void NotFoundHandle(string message)
        {
            ActionResult = new NotFoundObjectResult(message);
        }

        public void ActiveRentalConflictHandle(string message)
        {
            ActionResult = new ConflictObjectResult(message);
        }
    }
}
