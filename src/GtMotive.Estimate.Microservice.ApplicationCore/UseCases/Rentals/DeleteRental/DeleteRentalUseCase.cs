using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.DeleteRental
{
    public interface IDeleteRentalUseCase : IUseCase<DeleteRentalInput>
    {
    }

    public interface IDeleteRentalOutputPort
    {
        void StandardHandle();

        void NotFoundHandle(string message);
    }

    public sealed class DeleteRentalInput : IUseCaseInput
    {
        public DeleteRentalInput(Guid rentalId)
        {
            RentalId = rentalId;
        }

        public Guid RentalId { get; }
    }

    public sealed class DeleteRentalUseCase : IDeleteRentalUseCase
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDeleteRentalOutputPort _outputPort;

        public DeleteRentalUseCase(
            IRentalRepository rentalRepository,
            IUnitOfWork unitOfWork,
            IDeleteRentalOutputPort outputPort)
        {
            _rentalRepository = rentalRepository;
            _unitOfWork = unitOfWork;
            _outputPort = outputPort;
        }

        public async Task Execute(DeleteRentalInput input)
        {
            var rental = await _rentalRepository.GetById(input.RentalId);
            if (rental is null)
            {
                _outputPort.NotFoundHandle($"Rental '{input.RentalId}' was not found.");
                return;
            }

            await _rentalRepository.Remove(rental);
            await _unitOfWork.Save();
            _outputPort.StandardHandle();
        }
    }
}
