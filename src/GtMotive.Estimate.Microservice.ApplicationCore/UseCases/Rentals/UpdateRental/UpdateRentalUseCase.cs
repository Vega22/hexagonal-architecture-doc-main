using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.UpdateRental
{
    public interface IUpdateRentalUseCase : IUseCase<UpdateRentalInput>
    {
    }

    public interface IUpdateRentalOutputPort
    {
        void StandardHandle(UpdateRentalOutput output);

        void NotFoundHandle(string message);

        void ConflictHandle(string message);
    }

    public sealed class UpdateRentalInput : IUseCaseInput
    {
        public UpdateRentalInput(Guid rentalId, VehicleId vehicleId, CustomerId customerId, DateTime startAtUtc, DateTime? endAtUtc)
        {
            RentalId = rentalId;
            VehicleId = vehicleId;
            CustomerId = customerId;
            StartAtUtc = startAtUtc;
            EndAtUtc = endAtUtc;
        }

        public Guid RentalId { get; }

        public VehicleId VehicleId { get; }

        public CustomerId CustomerId { get; }

        public DateTime StartAtUtc { get; }

        public DateTime? EndAtUtc { get; }
    }

    public sealed class UpdateRentalOutput : IUseCaseOutput
    {
        public UpdateRentalOutput(Guid rentalId, Guid vehicleId, Guid customerId, DateTime startAtUtc, DateTime? endAtUtc, bool isActive)
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

    public sealed class UpdateRentalUseCase : IUpdateRentalUseCase
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUpdateRentalOutputPort _outputPort;

        public UpdateRentalUseCase(
            IRentalRepository rentalRepository,
            IVehicleRepository vehicleRepository,
            ICustomerRepository customerRepository,
            IUnitOfWork unitOfWork,
            IUpdateRentalOutputPort outputPort)
        {
            _rentalRepository = rentalRepository;
            _vehicleRepository = vehicleRepository;
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _outputPort = outputPort;
        }

        public async Task Execute(UpdateRentalInput input)
        {
            var rental = await _rentalRepository.GetById(input.RentalId);
            if (rental is null)
            {
                _outputPort.NotFoundHandle($"Rental '{input.RentalId}' was not found.");
                return;
            }

            var vehicle = await _vehicleRepository.GetById(input.VehicleId);
            if (vehicle is null)
            {
                _outputPort.NotFoundHandle($"Vehicle '{input.VehicleId}' was not found.");
                return;
            }

            var customer = await _customerRepository.GetById(input.CustomerId.Value);
            if (customer is null)
            {
                _outputPort.NotFoundHandle($"Customer '{input.CustomerId}' was not found.");
                return;
            }

            var isActive = input.EndAtUtc is null;
            if (isActive && await _rentalRepository.HasActiveRentalExcluding(input.CustomerId, input.RentalId))
            {
                _outputPort.ConflictHandle($"Customer '{input.CustomerId}' already has another active rental.");
                return;
            }

            if (isActive && await _rentalRepository.HasActiveRentalForVehicleExcluding(input.VehicleId, input.RentalId))
            {
                _outputPort.ConflictHandle($"Vehicle '{input.VehicleId}' already has another active rental.");
                return;
            }

            rental.Update(
                input.VehicleId.Value,
                customer.Id,
                input.StartAtUtc.ToUniversalTime(),
                input.EndAtUtc?.ToUniversalTime());

            await _unitOfWork.Save();
            _outputPort.StandardHandle(
                new UpdateRentalOutput(
                    rental.Id,
                    rental.VehicleId,
                    rental.CustomerId,
                    rental.StartAtUtc,
                    rental.EndAtUtc,
                    rental.IsActive));
        }
    }
}
