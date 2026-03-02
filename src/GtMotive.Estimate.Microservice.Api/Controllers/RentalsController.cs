using System.ComponentModel.DataAnnotations;
using GtMotive.Estimate.Microservice.Api.UseCases.Rentals;
using GtMotive.Estimate.Microservice.Api.ViewModels;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers
{
    /// <summary>
    /// Rentals endpoints.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public sealed class RentalsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAppLogger<RentalsController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentalsController"/> class.
        /// </summary>
        public RentalsController(IMediator mediator, IAppLogger<RentalsController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Rents a vehicle for a customer.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(RentVehicleResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Rent([FromBody][Required] RentVehicleRequestModel request)
        {
            _logger.LogInformation("Renting vehicle {vehicleId} to customer {customerId}.", request.VehicleId, request.CustomerId);
            var presenter = await _mediator.Send(new RentVehicleRequest(request.VehicleId!.Value, request.CustomerId!.Value, request.ReservedFromUtc));
            return presenter.ActionResult;
        }

        /// <summary>
        /// Returns a rented vehicle.
        /// </summary>
        [HttpPost("return")]
        [ProducesResponseType(typeof(ReturnVehicleResponseModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Return([FromBody][Required] ReturnVehicleRequestModel request)
        {
            _logger.LogInformation("Returning vehicle {vehicleId}.", request.VehicleId);
            var presenter = await _mediator.Send(new ReturnVehicleRequest(request.VehicleId));
            return presenter.ActionResult;
        }

        /// <summary>
        /// Lists rentals.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<RentalModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var presenter = await _mediator.Send(new ListRentalsRequest());
            return presenter.ActionResult;
        }

        /// <summary>
        /// Gets rental by id.
        /// </summary>
        /// <param name="rentalId">Rental id.</param>
        [HttpGet("{rentalId:guid}")]
        [ProducesResponseType(typeof(RentalModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid rentalId)
        {
            var presenter = await _mediator.Send(new GetRentalRequest(rentalId));
            return presenter.ActionResult;
        }

        /// <summary>
        /// Updates a rental.
        /// </summary>
        /// <param name="rentalId">Rental id.</param>
        /// <param name="request">Request body.</param>
        [HttpPut("{rentalId:guid}")]
        [ProducesResponseType(typeof(RentalModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Put([FromRoute] Guid rentalId, [FromBody][Required] UpdateRentalRequestModel request)
        {
            var presenter = await _mediator.Send(
                new UpdateRentalRequest(
                    rentalId,
                    request.VehicleId!.Value,
                    request.CustomerId!.Value,
                    request.StartAtUtc,
                    request.EndAtUtc));
            return presenter.ActionResult;
        }

        /// <summary>
        /// Deletes a rental.
        /// </summary>
        /// <param name="rentalId">Rental id.</param>
        [HttpDelete("{rentalId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid rentalId)
        {
            var presenter = await _mediator.Send(new DeleteRentalRequest(rentalId));
            return presenter.ActionResult;
        }
    }
}
