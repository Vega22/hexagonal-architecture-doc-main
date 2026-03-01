using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.ListRentals;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Rentals
{
    public sealed class ListRentalsRequest : IRequest<IWebApiPresenter>
    {
    }

    public sealed class ListRentalsRequestHandler : IRequestHandler<ListRentalsRequest, IWebApiPresenter>
    {
        private readonly IListRentalsUseCase _useCase;
        private readonly ListRentalsPresenter _presenter;

        public ListRentalsRequestHandler(IListRentalsUseCase useCase, ListRentalsPresenter presenter)
        {
            _useCase = useCase;
            _presenter = presenter;
        }

        public async Task<IWebApiPresenter> Handle(ListRentalsRequest request, CancellationToken cancellationToken)
        {
            await _useCase.Execute(new ListRentalsInput());
            return _presenter;
        }
    }
}
