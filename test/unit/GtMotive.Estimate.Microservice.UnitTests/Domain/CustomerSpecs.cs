using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.Entities;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.Domain
{
    public sealed class CustomerSpecs
    {
        [Fact]
        public void Create_WhenEmailIsInvalid_ShouldThrowDomainException()
        {
            var act = () => Customer.Create("John Doe", "ES", "DNI", "12345678Z", "invalid-email", "+34123456789");
            Assert.Throws<DomainException>(act);
        }

        [Fact]
        public void Create_WhenPhoneIsInvalid_ShouldThrowDomainException()
        {
            var act = () => Customer.Create("John Doe", "ES", "DNI", "12345678Z", "john@doe.com", "5551234");
            Assert.Throws<DomainException>(act);
        }

        [Fact]
        public void Create_WhenSpanishDocumentIsInvalid_ShouldThrowDomainException()
        {
            var act = () => Customer.Create("John Doe", "ES", "DNI", "12345678A", "john@doe.com", "+34123456789");
            Assert.Throws<DomainException>(act);
        }

        [Fact]
        public void Create_WhenSpanishDniIsValid_ShouldCreateCustomer()
        {
            var customer = Customer.Create("John Doe", "ES", "DNI", "12345678Z", "john@doe.com", "+34123456789");
            Assert.Equal("12345678Z", customer.DocumentNumber);
        }

        [Fact]
        public void Create_WhenSpanishNieIsValid_ShouldCreateCustomer()
        {
            var customer = Customer.Create("John Doe", "ES", "NIE", "X1234567L", "john@doe.com", "+34123456789");
            Assert.Equal("X1234567L", customer.DocumentNumber);
        }

        [Fact]
        public void Create_WhenSpanishCifIsValid_ShouldCreateCustomer()
        {
            var customer = Customer.Create("Empresa Demo", "ES", "CIF", "A58818501", "empresa@demo.com", "+34911111111");
            Assert.Equal("A58818501", customer.DocumentNumber);
        }
    }
}
