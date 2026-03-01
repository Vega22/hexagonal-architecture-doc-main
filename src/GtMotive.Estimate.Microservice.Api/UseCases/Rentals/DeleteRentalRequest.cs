using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.DeleteRental;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Rentals
{
    public sealed class DeleteRentalRequest : IRequest<IWebApiPresenter>
    {
        public DeleteRentalRequest(Guid rentalId)
        {
            RentalId = rentalId;
        }

        public Guid RentalId { get; }
    }

    public sealed class DeleteRentalRequestHandler : IRequestHandler<DeleteRentalRequest, IWebApiPresenter>
    {
        private readonly IDeleteRentalUseCase _useCase;
        private readonly DeleteRentalPresenter _presenter;

        public DeleteRentalRequestHandler(IDeleteRentalUseCase useCase, DeleteRentalPresenter presenter)
        {
            _useCase = useCase;
            _presenter = presenter;
        }

        public async Task<IWebApiPresenter> Handle(DeleteRentalRequest request, CancellationToken cancellationToken)
        {
            await _useCase.Execute(new DeleteRentalInput(request.RentalId));
            return _presenter;
        }
    }
}
