using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.ReturnVehicle;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Rentals
{
    /// <summary>
    /// Request handler for return vehicle endpoint.
    /// </summary>
    public sealed class ReturnVehicleRequestHandler : IRequestHandler<ReturnVehicleRequest, IWebApiPresenter>
    {
        private readonly IReturnVehicleUseCase _useCase;
        private readonly ReturnVehiclePresenter _presenter;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnVehicleRequestHandler"/> class.
        /// </summary>
        public ReturnVehicleRequestHandler(
            IReturnVehicleUseCase useCase,
            ReturnVehiclePresenter presenter)
        {
            _useCase = useCase;
            _presenter = presenter;
        }

        /// <inheritdoc/>
        public async Task<IWebApiPresenter> Handle(ReturnVehicleRequest request, CancellationToken cancellationToken)
        {
            await _useCase.Execute(new ReturnVehicleInput(new VehicleId(request.VehicleId)));
            return _presenter;
        }
    }
}
