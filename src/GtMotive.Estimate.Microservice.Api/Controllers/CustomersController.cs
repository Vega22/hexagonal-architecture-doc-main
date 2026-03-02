using System.ComponentModel.DataAnnotations;
using GtMotive.Estimate.Microservice.Api.UseCases.Customers;
using GtMotive.Estimate.Microservice.Api.ViewModels;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public sealed class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAppLogger<CustomersController> _logger;

        public CustomersController(IMediator mediator, IAppLogger<CustomersController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerModel), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Post([FromBody][Required] CreateCustomerRequestModel request)
        {
            _logger.LogInformation("Creating customer with document {document}.", request.DocumentNumber);
            var presenter = await _mediator.Send(
                new CreateCustomerRequest(
                    request.FullName,
                    request.CountryCode,
                    request.DocumentType,
                    request.DocumentNumber,
                    request.Email,
                    request.Phone));
            return presenter.ActionResult;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<CustomerModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] bool includeDeleted = false)
        {
            var presenter = await _mediator.Send(new ListCustomersRequest(includeDeleted));
            return presenter.ActionResult;
        }

        [HttpGet("{customerId:guid}")]
        [ProducesResponseType(typeof(CustomerModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById([FromRoute] Guid customerId)
        {
            var presenter = await _mediator.Send(new GetCustomerRequest(customerId));
            return presenter.ActionResult;
        }

        [HttpPut("{customerId:guid}")]
        [ProducesResponseType(typeof(CustomerModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Put([FromRoute] Guid customerId, [FromBody][Required] UpdateCustomerRequestModel request)
        {
            var presenter = await _mediator.Send(
                new UpdateCustomerRequest(
                    customerId,
                    request.FullName,
                    request.CountryCode,
                    request.DocumentType,
                    request.DocumentNumber,
                    request.Email,
                    request.Phone));
            return presenter.ActionResult;
        }

        [HttpDelete("{customerId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Delete([FromRoute] Guid customerId)
        {
            var presenter = await _mediator.Send(new DeleteCustomerRequest(customerId));
            return presenter.ActionResult;
        }
    }
}
