using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.RentVehicle;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Rentals
{
    /// <summary>
    /// Request handler for rent vehicle endpoint.
    /// </summary>
    public sealed class RentVehicleRequestHandler : IRequestHandler<RentVehicleRequest, IWebApiPresenter>
    {
        private readonly IRentVehicleUseCase _useCase;
        private readonly RentVehiclePresenter _presenter;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleRequestHandler"/> class.
        /// </summary>
        public RentVehicleRequestHandler(
            IRentVehicleUseCase useCase,
            RentVehiclePresenter presenter)
        {
            _useCase = useCase;
            _presenter = presenter;
        }

        /// <inheritdoc/>
        public async Task<IWebApiPresenter> Handle(RentVehicleRequest request, CancellationToken cancellationToken)
        {
            var reservedFromUtc = request.ReservedFromUtc ?? DateTime.UtcNow;
            var input = new RentVehicleInput(
                new VehicleId(request.VehicleId),
                new CustomerId(request.CustomerId),
                reservedFromUtc);
            await _useCase.Execute(input);
            return _presenter;
        }
    }
}
