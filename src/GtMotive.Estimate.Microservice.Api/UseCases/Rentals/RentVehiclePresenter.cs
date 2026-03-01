using GtMotive.Estimate.Microservice.Api.ViewModels;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.RentVehicle;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Rentals
{
    /// <summary>
    /// Presenter for rent vehicle use case.
    /// </summary>
    public sealed class RentVehiclePresenter : IWebApiPresenter, IRentVehicleOutputPort
    {
        /// <inheritdoc/>
        public IActionResult ActionResult { get; private set; }

        /// <inheritdoc/>
        public void StandardHandle(RentVehicleOutput response)
        {
            ActionResult = new OkObjectResult(new RentVehicleResponseModel(
                response.VehicleId,
                response.CustomerId,
                response.RentedAtUtc));
        }

        /// <inheritdoc/>
        public void NotFoundHandle(string message)
        {
            ActionResult = new NotFoundObjectResult(message);
        }

        /// <inheritdoc/>
        public void CustomerAlreadyHasActiveRentalHandle(string message)
        {
            ActionResult = new ConflictObjectResult(message);
        }

        /// <inheritdoc/>
        public void VehicleUnavailableHandle(string message)
        {
            ActionResult = new ConflictObjectResult(message);
        }
    }
}
