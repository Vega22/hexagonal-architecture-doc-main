using System;
using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.Domain
{
    public sealed class AggregateGuardsSpecs
    {
        [Fact]
        public void Vehicle_MarkDeleted_WhenTimestampIsDefault_ShouldThrowDomainException()
        {
            var vehicle = Vehicle.Create(
                new LicensePlate("1234ABC"),
                new ManufactureDate(DateOnly.FromDateTime(DateTime.UtcNow)),
                "Toyota",
                "Yaris");

            var act = () => vehicle.MarkDeleted(default);

            Assert.Throws<DomainException>(act);
        }

        [Fact]
        public void Rental_Create_WhenStartDateIsDefault_ShouldThrowDomainException()
        {
            var act = () => Rental.Create(Guid.NewGuid(), Guid.NewGuid(), default);

            Assert.Throws<DomainException>(act);
        }

        [Fact]
        public void Rental_Complete_WhenReturnDateIsDefault_ShouldThrowDomainException()
        {
            var rental = Rental.Create(Guid.NewGuid(), Guid.NewGuid(), DateTime.UtcNow);
            var act = () => rental.Complete(default);

            Assert.Throws<DomainException>(act);
        }
    }
}
