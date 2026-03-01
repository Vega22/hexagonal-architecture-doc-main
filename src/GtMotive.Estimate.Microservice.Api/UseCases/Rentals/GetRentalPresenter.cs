using GtMotive.Estimate.Microservice.Api.ViewModels;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.GetRental;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Rentals
{
    public sealed class GetRentalPresenter : IWebApiPresenter, IGetRentalOutputPort
    {
        public IActionResult ActionResult { get; private set; }

        public void StandardHandle(GetRentalOutput output)
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
    }
}
