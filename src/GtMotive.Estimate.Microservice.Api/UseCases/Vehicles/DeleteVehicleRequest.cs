using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.DeleteVehicle;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicles
{
    public sealed class DeleteVehicleRequest : IRequest<IWebApiPresenter>
    {
        public DeleteVehicleRequest(Guid vehicleId)
        {
            VehicleId = vehicleId;
        }

        public Guid VehicleId { get; }
    }

    public sealed class DeleteVehicleRequestHandler : IRequestHandler<DeleteVehicleRequest, IWebApiPresenter>
    {
        private readonly IDeleteVehicleUseCase _useCase;
        private readonly DeleteVehiclePresenter _presenter;

        public DeleteVehicleRequestHandler(IDeleteVehicleUseCase useCase, DeleteVehiclePresenter presenter)
        {
            _useCase = useCase;
            _presenter = presenter;
        }

        public async Task<IWebApiPresenter> Handle(DeleteVehicleRequest request, CancellationToken cancellationToken)
        {
            await _useCase.Execute(new DeleteVehicleInput(new VehicleId(request.VehicleId)));
            return _presenter;
        }
    }
}
