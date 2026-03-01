using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.GetRental;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Rentals
{
    public sealed class GetRentalRequest : IRequest<IWebApiPresenter>
    {
        public GetRentalRequest(Guid rentalId)
        {
            RentalId = rentalId;
        }

        public Guid RentalId { get; }
    }

    public sealed class GetRentalRequestHandler : IRequestHandler<GetRentalRequest, IWebApiPresenter>
    {
        private readonly IGetRentalUseCase _useCase;
        private readonly GetRentalPresenter _presenter;

        public GetRentalRequestHandler(IGetRentalUseCase useCase, GetRentalPresenter presenter)
        {
            _useCase = useCase;
            _presenter = presenter;
        }

        public async Task<IWebApiPresenter> Handle(GetRentalRequest request, CancellationToken cancellationToken)
        {
            await _useCase.Execute(new GetRentalInput(request.RentalId));
            return _presenter;
        }
    }
}
