using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Customers.GetCustomer;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Customers
{
    public sealed class GetCustomerRequest : IRequest<IWebApiPresenter>
    {
        public GetCustomerRequest(Guid customerId)
        {
            CustomerId = customerId;
        }

        public Guid CustomerId { get; }
    }

    public sealed class GetCustomerRequestHandler : IRequestHandler<GetCustomerRequest, IWebApiPresenter>
    {
        private readonly IGetCustomerUseCase _useCase;
        private readonly GetCustomerPresenter _presenter;

        public GetCustomerRequestHandler(IGetCustomerUseCase useCase, GetCustomerPresenter presenter)
        {
            _useCase = useCase;
            _presenter = presenter;
        }

        public async Task<IWebApiPresenter> Handle(GetCustomerRequest request, CancellationToken cancellationToken)
        {
            await _useCase.Execute(new GetCustomerInput(request.CustomerId));
            return _presenter;
        }
    }
}
