using GtMotive.Estimate.Microservice.Api.ViewModels;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.ListRentals;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Rentals
{
    public sealed class ListRentalsPresenter : IWebApiPresenter, IListRentalsOutputPort
    {
        public IActionResult ActionResult { get; private set; }

        public void StandardHandle(ListRentalsOutput output)
        {
            ActionResult = new OkObjectResult(
                output.Rentals
                    .Select(r => new RentalModel(
                        r.RentalId,
                        r.VehicleId,
                        r.CustomerId,
                        r.StartAtUtc,
                        r.EndAtUtc,
                        r.IsActive))
                    .ToList());
        }
    }
}
