using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.ListAvailableVehicles;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicles
{
    /// <summary>
    /// Request handler for available vehicles endpoint.
    /// </summary>
    public sealed class ListAvailableVehiclesRequestHandler : IRequestHandler<ListAvailableVehiclesRequest, IWebApiPresenter>
    {
        private readonly IListAvailableVehiclesUseCase _useCase;
        private readonly ListAvailableVehiclesPresenter _presenter;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListAvailableVehiclesRequestHandler"/> class.
        /// </summary>
        public ListAvailableVehiclesRequestHandler(
            IListAvailableVehiclesUseCase useCase,
            ListAvailableVehiclesPresenter presenter)
        {
            _useCase = useCase;
            _presenter = presenter;
        }

        /// <inheritdoc/>
        public async Task<IWebApiPresenter> Handle(ListAvailableVehiclesRequest request, CancellationToken cancellationToken)
        {
            await _useCase.Execute(new ListAvailableVehiclesInput());
            return _presenter;
        }
    }
}
