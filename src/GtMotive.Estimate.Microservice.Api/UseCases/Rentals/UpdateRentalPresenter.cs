using GtMotive.Estimate.Microservice.Api.ViewModels;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.UpdateRental;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Rentals
{
    public sealed class UpdateRentalPresenter : IWebApiPresenter, IUpdateRentalOutputPort
    {
        public IActionResult ActionResult { get; private set; }

        public void StandardHandle(UpdateRentalOutput output)
        {
            ActionResult = new OkObjectResult(
                new RentalModel(
                    output.RentalId,
                    output.VehicleId,
                    output.CustomerId,
                    output.StartAtUtc,
                    output.EndAtUtc,
                    output.IsActive));
        }

        public void NotFoundHandle(string message)
        {
            ActionResult = new NotFoundObjectResult(message);
        }

        public void ConflictHandle(string message)
        {
            ActionResult = new ConflictObjectResult(message);
        }
    }
}
