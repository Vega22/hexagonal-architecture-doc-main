using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.GetRental
{
    public interface IGetRentalUseCase : IUseCase<GetRentalInput>
    {
    }

    public interface IGetRentalOutputPort
    {
        void StandardHandle(GetRentalOutput output);

        void NotFoundHandle(string message);
    }

    public sealed class GetRentalInput : IUseCaseInput
    {
        public GetRentalInput(Guid rentalId)
        {
            RentalId = rentalId;
        }

        public Guid RentalId { get; }
    }

    public sealed class GetRentalOutput : IUseCaseOutput
    {
        public GetRentalOutput(Guid rentalId, Guid vehicleId, string customerId, DateTime startAtUtc, DateTime? endAtUtc, bool isActive)
        {
            RentalId = rentalId;
            VehicleId = vehicleId;
            CustomerId = customerId;
            StartAtUtc = startAtUtc;
            EndAtUtc = endAtUtc;
            IsActive = isActive;
        }

        public Guid RentalId { get; }

        public Guid VehicleId { get; }

        public string CustomerId { get; }

        public DateTime StartAtUtc { get; }

        public DateTime? EndAtUtc { get; }

        public bool IsActive { get; }
    }

    public sealed class GetRentalUseCase : IGetRentalUseCase
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IGetRentalOutputPort _outputPort;

        public GetRentalUseCase(IRentalRepository rentalRepository, IGetRentalOutputPort outputPort)
        {
            _rentalRepository = rentalRepository;
            _outputPort = outputPort;
        }

        public async Task Execute(GetRentalInput input)
        {
            var rental = await _rentalRepository.GetById(input.RentalId);
            if (rental is null)
            {
                _outputPort.NotFoundHandle($"Rental '{input.RentalId}' was not found.");
                return;
            }

            _outputPort.StandardHandle(
                new GetRentalOutput(
                    rental.Id,
                    rental.VehicleId,
                    rental.CustomerId,
                    rental.StartAtUtc,
                    rental.EndAtUtc,
                    rental.IsActive));
        }
    }
}
