using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.CreateVehicle;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicles
{
    /// <summary>
    /// Request handler for create vehicle endpoint.
    /// </summary>
    public sealed class CreateVehicleRequestHandler : IRequestHandler<CreateVehicleRequest, IWebApiPresenter>
    {
        private readonly ICreateVehicleUseCase _useCase;
        private readonly CreateVehiclePresenter _presenter;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVehicleRequestHandler"/> class.
        /// </summary>
        public CreateVehicleRequestHandler(
            ICreateVehicleUseCase useCase,
            CreateVehiclePresenter presenter)
        {
            _useCase = useCase;
            _presenter = presenter;
        }

        /// <inheritdoc/>
        public async Task<IWebApiPresenter> Handle(CreateVehicleRequest request, CancellationToken cancellationToken)
        {
            var input = new CreateVehicleInput(
                new LicensePlate(request.LicensePlate),
                new ManufactureDate(request.ManufactureDate),
                request.Brand,
                request.Model);

            await _useCase.Execute(input);
            return _presenter;
        }
    }
}
