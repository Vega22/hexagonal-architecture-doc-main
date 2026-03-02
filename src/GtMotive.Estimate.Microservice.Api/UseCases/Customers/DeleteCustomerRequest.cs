using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Customers.DeleteCustomer;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Customers
{
    public sealed class DeleteCustomerRequest : IRequest<IWebApiPresenter>
    {
        public DeleteCustomerRequest(Guid customerId)
        {
            CustomerId = customerId;
        }

        public Guid CustomerId { get; }
    }

    public sealed class DeleteCustomerRequestHandler : IRequestHandler<DeleteCustomerRequest, IWebApiPresenter>
    {
        private readonly IDeleteCustomerUseCase _useCase;
        private readonly DeleteCustomerPresenter _presenter;

        public DeleteCustomerRequestHandler(IDeleteCustomerUseCase useCase, DeleteCustomerPresenter presenter)
        {
            _useCase = useCase;
            _presenter = presenter;
        }

        public async Task<IWebApiPresenter> Handle(DeleteCustomerRequest request, CancellationToken cancellationToken)
        {
            await _useCase.Execute(new DeleteCustomerInput(request.CustomerId));
            return _presenter;
        }
    }
}
