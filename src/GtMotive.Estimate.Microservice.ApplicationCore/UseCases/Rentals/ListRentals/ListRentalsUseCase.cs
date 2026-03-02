using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.ListRentals
{
    public interface IListRentalsUseCase : IUseCase<ListRentalsInput>
    {
    }

    public interface IListRentalsOutputPort
    {
        void StandardHandle(ListRentalsOutput output);
    }

    public sealed class ListRentalsInput : IUseCaseInput
    {
    }

    public sealed class RentalListItemOutput
    {
        public RentalListItemOutput(Guid rentalId, Guid vehicleId, Guid customerId, DateTime startAtUtc, DateTime? endAtUtc, bool isActive)
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

        public Guid CustomerId { get; }

        public DateTime StartAtUtc { get; }

        public DateTime? EndAtUtc { get; }

        public bool IsActive { get; }
    }

    public sealed class ListRentalsOutput : IUseCaseOutput
    {
        public ListRentalsOutput(IReadOnlyCollection<RentalListItemOutput> rentals)
        {
            Rentals = rentals;
        }

        public IReadOnlyCollection<RentalListItemOutput> Rentals { get; }
    }

    public sealed class ListRentalsUseCase : IListRentalsUseCase
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IListRentalsOutputPort _outputPort;

        public ListRentalsUseCase(IRentalRepository rentalRepository, IListRentalsOutputPort outputPort)
        {
            _rentalRepository = rentalRepository;
            _outputPort = outputPort;
        }

        public async Task Execute(ListRentalsInput input)
        {
            var rentals = await _rentalRepository.List();
            _outputPort.StandardHandle(
                new ListRentalsOutput(
                    rentals
                        .Select(r => new RentalListItemOutput(
                            r.Id,
                            r.VehicleId,
                            r.CustomerId,
                            r.StartAtUtc,
                            r.EndAtUtc,
                            r.IsActive))
                        .ToList()));
        }
    }
}
