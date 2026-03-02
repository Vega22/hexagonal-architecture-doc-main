using GtMotive.Estimate.Microservice.Api.ViewModels;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Customers.CreateCustomer;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Customers
{
    public sealed class CreateCustomerPresenter : IWebApiPresenter, ICreateCustomerOutputPort
    {
        public IActionResult ActionResult { get; private set; } = default!;

        public void StandardHandle(CreateCustomerOutput output)
        {
            var body = new CustomerModel(
                output.CustomerId,
                output.FullName,
                output.CountryCode,
                output.DocumentType,
                output.DocumentNumber,
                output.Email,
                output.Phone,
                false,
                output.CreatedAtUtc,
                output.UpdatedAtUtc);
            ActionResult = new CreatedResult($"/api/customers/{output.CustomerId}", body);
        }

        public void ConflictHandle(string message)
        {
            ActionResult = new ConflictObjectResult(message);
        }
    }
}
