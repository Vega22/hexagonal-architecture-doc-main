using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.UpdateRental;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Rentals
{
    public sealed class UpdateRentalRequest : IRequest<IWebApiPresenter>
    {
        public UpdateRentalRequest(Guid rentalId, Guid vehicleId, Guid customerId, DateTime startAtUtc, DateTime? endAtUtc)
        {
            RentalId = rentalId;
            VehicleId = vehicleId;
            CustomerId = customerId;
            StartAtUtc = startAtUtc;
            EndAtUtc = endAtUtc;
        }

        public Guid RentalId { get; }

        public Guid VehicleId { get; }

        public Guid CustomerId { get; }

        public DateTime StartAtUtc { get; }

        public DateTime? EndAtUtc { get; }
    }

    public sealed class UpdateRentalRequestHandler : IRequestHandler<UpdateRentalRequest, IWebApiPresenter>
    {
        private readonly IUpdateRentalUseCase _useCase;
        private readonly UpdateRentalPresenter _presenter;

        public UpdateRentalRequestHandler(IUpdateRentalUseCase useCase, UpdateRentalPresenter presenter)
        {
            _useCase = useCase;
            _presenter = presenter;
        }

        public async Task<IWebApiPresenter> Handle(UpdateRentalRequest request, CancellationToken cancellationToken)
        {
            await _useCase.Execute(
                new UpdateRentalInput(
                    request.RentalId,
                    new VehicleId(request.VehicleId),
                    new CustomerId(request.CustomerId),
                    request.StartAtUtc,
                    request.EndAtUtc));
            return _presenter;
        }
    }
}
