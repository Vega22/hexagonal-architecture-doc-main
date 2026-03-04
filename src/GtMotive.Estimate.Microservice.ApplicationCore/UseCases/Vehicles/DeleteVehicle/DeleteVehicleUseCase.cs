using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.DeleteVehicle
{
    public interface IDeleteVehicleUseCase : IUseCase<DeleteVehicleInput>
    {
    }

    public interface IDeleteVehicleOutputPort
    {
        void StandardHandle();

        void NotFoundHandle(string message);

        void ActiveRentalConflictHandle(string message);
    }

    public sealed class DeleteVehicleInput : IUseCaseInput
    {
        public DeleteVehicleInput(VehicleId vehicleId)
        {
            VehicleId = vehicleId;
        }

        public VehicleId VehicleId { get; }
    }

    public sealed class DeleteVehicleUseCase : IDeleteVehicleUseCase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDeleteVehicleOutputPort _outputPort;

        public DeleteVehicleUseCase(
            IVehicleRepository vehicleRepository,
            IRentalRepository rentalRepository,
            IUnitOfWork unitOfWork,
            IDeleteVehicleOutputPort outputPort)
        {
            _vehicleRepository = vehicleRepository;
            _rentalRepository = rentalRepository;
            _unitOfWork = unitOfWork;
            _outputPort = outputPort;
        }

        public async Task Execute(DeleteVehicleInput input)
        {
            await _unitOfWork.BeginTransaction();

            try
            {
                var vehicle = await _vehicleRepository.GetById(input.VehicleId);
                if (vehicle is null)
                {
                    await _unitOfWork.RollbackTransaction();
                    _outputPort.NotFoundHandle($"Vehicle '{input.VehicleId}' was not found.");
                    return;
                }

                if (await _rentalRepository.HasActiveRentalForVehicle(input.VehicleId))
                {
                    await _unitOfWork.RollbackTransaction();
                    _outputPort.ActiveRentalConflictHandle(
                        $"Vehicle '{input.VehicleId}' cannot be deleted because it has an active rental.");
                    return;
                }

                vehicle.MarkDeleted(DateTime.UtcNow);
                await _unitOfWork.Save();
                await _unitOfWork.CommitTransaction();
                _outputPort.StandardHandle();
            }
            catch
            {
                await _unitOfWork.RollbackTransaction();
                throw;
            }
        }
    }
}
