using GtMotive.Estimate.Microservice.Api.ViewModels;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Customers.ListCustomers;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Customers
{
    public sealed class ListCustomersPresenter : IWebApiPresenter, IListCustomersOutputPort
    {
        public IActionResult ActionResult { get; private set; } = default!;

        public void StandardHandle(ListCustomersOutput output)
        {
            ActionResult = new OkObjectResult(
                output.Customers.Select(c => new CustomerModel(
                    c.CustomerId,
                    c.FullName,
                    c.CountryCode,
                    c.DocumentType,
                    c.DocumentNumber,
                    c.Email,
                    c.Phone,
                    c.IsDeleted,
                    c.CreatedAtUtc,
                    c.UpdatedAtUtc)).ToList());
        }
    }
}
