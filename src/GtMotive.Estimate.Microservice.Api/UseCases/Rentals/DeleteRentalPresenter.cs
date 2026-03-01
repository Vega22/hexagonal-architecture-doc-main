using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.DeleteRental;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Rentals
{
    public sealed class DeleteRentalPresenter : IWebApiPresenter, IDeleteRentalOutputPort
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
    }
}
