using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.RentVehicle;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using Moq;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.ApplicationCore
{
    /// <summary>
    /// Unit tests for rent vehicle use case.
    /// </summary>
    public sealed class RentVehicleUseCaseSpecs
    {
        /// <summary>
        /// Should reject rental when customer already has active rental.
        /// </summary>
        [Fact]
        public async Task Execute_WhenCustomerHasActiveRental_ShouldReturnConflictOutput()
        {
            var vehicleRepository = new Mock<IVehicleRepository>();
            var rentalRepository = new Mock<IRentalRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var outputPort = new Mock<IRentVehicleOutputPort>();

            var vehicle = Vehicle.Create(
                new LicensePlate("1234ABC"),
                new ManufactureDate(DateOnly.FromDateTime(DateTime.UtcNow)),
                "Toyota",
                "Yaris");

            vehicleRepository
                .Setup(repo => repo.GetById(It.IsAny<VehicleId>()))
                .ReturnsAsync(vehicle);

            rentalRepository
                .Setup(repo => repo.HasActiveRental(It.IsAny<CustomerId>()))
                .ReturnsAsync(true);

            var useCase = new RentVehicleUseCase(
                vehicleRepository.Object,
                rentalRepository.Object,
                unitOfWork.Object,
                outputPort.Object);

            await useCase.Execute(new RentVehicleInput(vehicle.Id, new CustomerId("customer-1")));

            outputPort.Verify(port => port.CustomerAlreadyHasActiveRentalHandle(It.IsAny<string>()), Times.Once);
            unitOfWork.Verify(work => work.Save(), Times.Never);
        }
    }
}
