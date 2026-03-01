using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.ListVehicles;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicles
{
    public sealed class ListVehiclesRequest : IRequest<IWebApiPresenter>
    {
        public ListVehiclesRequest(bool includeDeleted, bool onlyOverFiveYears = false)
        {
            IncludeDeleted = includeDeleted;
            OnlyOverFiveYears = onlyOverFiveYears;
        }

        public bool IncludeDeleted { get; }

        public bool OnlyOverFiveYears { get; }
    }

    public sealed class ListVehiclesRequestHandler : IRequestHandler<ListVehiclesRequest, IWebApiPresenter>
    {
        private readonly IListVehiclesUseCase _useCase;
        private readonly ListVehiclesPresenter _presenter;

        public ListVehiclesRequestHandler(IListVehiclesUseCase useCase, ListVehiclesPresenter presenter)
        {
            _useCase = useCase;
            _presenter = presenter;
        }

        public async Task<IWebApiPresenter> Handle(ListVehiclesRequest request, CancellationToken cancellationToken)
        {
            await _useCase.Execute(new ListVehiclesInput(request.IncludeDeleted, request.OnlyOverFiveYears));
            return _presenter;
        }
    }
}
