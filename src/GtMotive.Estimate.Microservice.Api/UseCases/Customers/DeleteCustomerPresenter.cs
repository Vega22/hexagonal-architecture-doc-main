using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Customers.DeleteCustomer;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Customers
{
    public sealed class DeleteCustomerPresenter : IWebApiPresenter, IDeleteCustomerOutputPort
    {
        public IActionResult ActionResult { get; private set; } = default!;

        public void StandardHandle()
        {
            ActionResult = new NoContentResult();
        }

        public void NotFoundHandle(string message)
        {
            ActionResult = new NotFoundObjectResult(message);
        }

        public void ConflictHandle(string message)
        {
            ActionResult = new ConflictObjectResult(message);
        }
    }
}
