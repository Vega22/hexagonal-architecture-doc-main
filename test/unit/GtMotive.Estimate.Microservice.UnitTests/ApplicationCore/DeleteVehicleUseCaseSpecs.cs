using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.DeleteVehicle;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using Moq;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.ApplicationCore
{
    /// <summary>
    /// Unit tests for delete vehicle use case.
    /// </summary>
    public sealed class DeleteVehicleUseCaseSpecs
    {
        /// <summary>
        /// Should soft delete vehicle and persist changes.
        /// </summary>
        [Fact]
        public async Task Execute_WhenVehicleExists_ShouldSoftDeleteVehicle()
        {
            var vehicleRepository = new Mock<IVehicleRepository>();
            var rentalRepository = new Mock<IRentalRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var outputPort = new Mock<IDeleteVehicleOutputPort>();

            var vehicle = Vehicle.Create(
                new LicensePlate("1234ZZZ"),
                new ManufactureDate(DateOnly.FromDateTime(DateTime.UtcNow)),
                "Seat",
                "Ibiza");

            vehicleRepository.Setup(repo => repo.GetById(vehicle.Id)).ReturnsAsync(vehicle);
            rentalRepository.Setup(repo => repo.HasActiveRentalForVehicle(vehicle.Id)).ReturnsAsync(false);

            var useCase = new DeleteVehicleUseCase(
                vehicleRepository.Object,
                rentalRepository.Object,
                unitOfWork.Object,
                outputPort.Object);

            await useCase.Execute(new DeleteVehicleInput(vehicle.Id));

            Assert.True(vehicle.IsDeleted);
            Assert.NotNull(vehicle.DeletedAtUtc);
            unitOfWork.Verify(work => work.Save(), Times.Once);
            outputPort.Verify(port => port.StandardHandle(), Times.Once);
        }
    }
}
