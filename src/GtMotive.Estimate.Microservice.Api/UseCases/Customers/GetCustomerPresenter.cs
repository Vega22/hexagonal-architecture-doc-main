using GtMotive.Estimate.Microservice.Api.ViewModels;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Customers.GetCustomer;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Customers
{
    public sealed class GetCustomerPresenter : IWebApiPresenter, IGetCustomerOutputPort
    {
        public IActionResult ActionResult { get; private set; } = default!;

        public void StandardHandle(GetCustomerOutput output)
        {
            ActionResult = new OkObjectResult(
                new CustomerModel(
                    output.CustomerId,
                    output.FullName,
                    output.CountryCode,
                    output.DocumentType,
                    output.DocumentNumber,
                    output.Email,
                    output.Phone,
                    output.IsDeleted,
                    output.CreatedAtUtc,
                    output.UpdatedAtUtc));
        }

        public void NotFoundHandle(string message)
        {
            ActionResult = new NotFoundObjectResult(message);
        }
    }
}
