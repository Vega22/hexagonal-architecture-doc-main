using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.UpdateVehicle;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicles
{
    public sealed class UpdateVehicleRequest : IRequest<IWebApiPresenter>
    {
        public UpdateVehicleRequest(Guid vehicleId, string licensePlate, DateOnly manufactureDate, string brand, string model)
        {
            VehicleId = vehicleId;
            LicensePlate = licensePlate;
            ManufactureDate = manufactureDate;
            Brand = brand;
            Model = model;
        }

        public Guid VehicleId { get; }

        public string LicensePlate { get; }

        public DateOnly ManufactureDate { get; }

        public string Brand { get; }

        public string Model { get; }
    }

    public sealed class UpdateVehicleRequestHandler : IRequestHandler<UpdateVehicleRequest, IWebApiPresenter>
    {
        private readonly IUpdateVehicleUseCase _useCase;
        private readonly UpdateVehiclePresenter _presenter;

        public UpdateVehicleRequestHandler(IUpdateVehicleUseCase useCase, UpdateVehiclePresenter presenter)
        {
            _useCase = useCase;
            _presenter = presenter;
        }

        public async Task<IWebApiPresenter> Handle(UpdateVehicleRequest request, CancellationToken cancellationToken)
        {
            await _useCase.Execute(
                new UpdateVehicleInput(
                    new VehicleId(request.VehicleId),
                    new LicensePlate(request.LicensePlate),
                    new ManufactureDate(request.ManufactureDate),
                    request.Brand,
                    request.Model));
            return _presenter;
        }
    }
}
