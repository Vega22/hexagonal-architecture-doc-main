using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Customers.ListCustomers;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Customers
{
    public sealed class ListCustomersRequest : IRequest<IWebApiPresenter>
    {
        public ListCustomersRequest(bool includeDeleted)
        {
            IncludeDeleted = includeDeleted;
        }

        public bool IncludeDeleted { get; }
    }

    public sealed class ListCustomersRequestHandler : IRequestHandler<ListCustomersRequest, IWebApiPresenter>
    {
        private readonly IListCustomersUseCase _useCase;
        private readonly ListCustomersPresenter _presenter;

        public ListCustomersRequestHandler(IListCustomersUseCase useCase, ListCustomersPresenter presenter)
        {
            _useCase = useCase;
            _presenter = presenter;
        }

        public async Task<IWebApiPresenter> Handle(ListCustomersRequest request, CancellationToken cancellationToken)
        {
            await _useCase.Execute(new ListCustomersInput(request.IncludeDeleted));
            return _presenter;
        }
    }
}
