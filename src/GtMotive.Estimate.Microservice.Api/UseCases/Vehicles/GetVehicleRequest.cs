using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.GetVehicle;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicles
{
    public sealed class GetVehicleRequest : IRequest<IWebApiPresenter>
    {
        public GetVehicleRequest(Guid vehicleId)
        {
            VehicleId = vehicleId;
        }

        public Guid VehicleId { get; }
    }

    public sealed class GetVehicleRequestHandler : IRequestHandler<GetVehicleRequest, IWebApiPresenter>
    {
        private readonly IGetVehicleUseCase _useCase;
        private readonly GetVehiclePresenter _presenter;

        public GetVehicleRequestHandler(IGetVehicleUseCase useCase, GetVehiclePresenter presenter)
        {
            _useCase = useCase;
            _presenter = presenter;
        }

        public async Task<IWebApiPresenter> Handle(GetVehicleRequest request, CancellationToken cancellationToken)
        {
            await _useCase.Execute(new GetVehicleInput(new VehicleId(request.VehicleId)));
            return _presenter;
        }
    }
}
