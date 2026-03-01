using System.ComponentModel.DataAnnotations;
using GtMotive.Estimate.Microservice.Api.UseCases.Vehicles;
using GtMotive.Estimate.Microservice.Api.ViewModels;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers
{
    /// <summary>
    /// Vehicles endpoints.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public sealed class VehiclesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAppLogger<VehiclesController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="VehiclesController"/> class.
        /// </summary>
        public VehiclesController(IMediator mediator, IAppLogger<VehiclesController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Creates a vehicle for fleet.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CreateVehicleResponseModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody][Required] CreateVehicleRequestModel request)
        {
            _logger.LogInformation("Creating vehicle {brand} {model} with plate {plate}.", request.Brand, request.Model, request.LicensePlate);
            var presenter = await _mediator.Send(
                new CreateVehicleRequest(
                    request.LicensePlate,
                    request.ManufactureDate,
                    request.Brand,
                    request.Model));
            return presenter.ActionResult;
        }

        /// <summary>
        /// Lists available vehicles.
        /// </summary>
        [HttpGet("available")]
        [ProducesResponseType(typeof(IReadOnlyCollection<AvailableVehicleModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAvailable()
        {
            var presenter = await _mediator.Send(new ListAvailableVehiclesRequest());
            return presenter.ActionResult;
        }

        /// <summary>
        /// Lists vehicles.
        /// </summary>
        /// <param name="includeDeleted">Includes soft deleted records when true.</param>
        /// <param name="onlyOverFiveYears">Returns only vehicles older than five years when true.</param>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<VehicleModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] bool includeDeleted = false, [FromQuery] bool onlyOverFiveYears = false)
        {
            var presenter = await _mediator.Send(new ListVehiclesRequest(includeDeleted, onlyOverFiveYears));
            return presenter.ActionResult;
        }

        /// <summary>
        /// Lists vehicles with manufacture date older than five years.
        /// </summary>
        [HttpGet("over-five-years")]
        [ProducesResponseType(typeof(IReadOnlyCollection<VehicleModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetOverFiveYears()
        {
            var presenter = await _mediator.Send(new ListVehiclesRequest(includeDeleted: false, onlyOverFiveYears: true));
            return presenter.ActionResult;
        }

        /// <summary>
        /// Gets a vehicle by id.
        /// </summary>
        /// <param name="vehicleId">Vehicle id.</param>
        [HttpGet("{vehicleId:guid}")]
        [ProducesResponseType(typeof(VehicleModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid vehicleId)
        {
            var presenter = await _mediator.Send(new GetVehicleRequest(vehicleId));
            return presenter.ActionResult;
        }

        /// <summary>
        /// Updates a vehicle.
        /// </summary>
        /// <param name="vehicleId">Vehicle id.</param>
        /// <param name="request">Request body.</param>
        [HttpPut("{vehicleId:guid}")]
        [ProducesResponseType(typeof(VehicleModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Put([FromRoute] Guid vehicleId, [FromBody][Required] UpdateVehicleRequestModel request)
        {
            var presenter = await _mediator.Send(
                new UpdateVehicleRequest(
                    vehicleId,
                    request.LicensePlate,
                    request.ManufactureDate,
                    request.Brand,
                    request.Model));
            return presenter.ActionResult;
        }

        /// <summary>
        /// Soft deletes a vehicle.
        /// </summary>
        /// <param name="vehicleId">Vehicle id.</param>
        [HttpDelete("{vehicleId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Delete([FromRoute] Guid vehicleId)
        {
            var presenter = await _mediator.Send(new DeleteVehicleRequest(vehicleId));
            return presenter.ActionResult;
        }
    }
}
