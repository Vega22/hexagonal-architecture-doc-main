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
    public sealed class RentVehicleCustomerGuardsSpecs
    {
        [Fact]
        public async Task Execute_WhenCustomerDoesNotExist_ShouldReturnNotFound()
        {
            var vehicleRepository = new Mock<IVehicleRepository>();
            var customerRepository = new Mock<ICustomerRepository>();
            var rentalRepository = new Mock<IRentalRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();
            var outputPort = new Mock<IRentVehicleOutputPort>();
            var customerId = Guid.NewGuid();

            var vehicle = Vehicle.Create(
                new LicensePlate("1234ABC"),
                new ManufactureDate(DateOnly.FromDateTime(DateTime.UtcNow)),
                "Toyota",
                "Yaris");

            vehicleRepository.Setup(repo => repo.GetById(It.IsAny<VehicleId>())).ReturnsAsync(vehicle);
            customerRepository.Setup(repo => repo.GetById(customerId)).ReturnsAsync(default(Customer));

            var useCase = new RentVehicleUseCase(
                vehicleRepository.Object,
                customerRepository.Object,
                rentalRepository.Object,
                unitOfWork.Object,
                outputPort.Object);

            await useCase.Execute(new RentVehicleInput(vehicle.Id, new CustomerId(customerId)));

            outputPort.Verify(port => port.NotFoundHandle(It.Is<string>(message => message.Contains("Customer"))), Times.Once);
            unitOfWork.Verify(work => work.Save(), Times.Never);
        }
    }
}
