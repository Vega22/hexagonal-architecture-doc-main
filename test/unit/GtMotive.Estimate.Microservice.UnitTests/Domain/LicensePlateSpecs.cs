using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.Domain
{
    public sealed class LicensePlateSpecs
    {
        [Theory]
        [InlineData("1234ABC")] // Spain current
        [InlineData("M-1234-AB")] // Spain legacy
        [InlineData("AB-123-CD")] // France/Italy
        [InlineData("B-AB 1234")] // Germany
        [InlineData("AB12 CDE")] // UK
        [InlineData("12-AB-34")] // Portugal
        [InlineData("1-ABC-123")] // Belgium
        [InlineData("171-D-12345")] // Ireland
        public void Ctor_WhenFormatIsValidEuropean_ShouldCreatePlate(string value)
        {
            var plate = new LicensePlate(value);
            Assert.Equal(value.Trim().ToUpperInvariant(), plate.Value);
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("ABC")]
        [InlineData("1234567890123")]
        [InlineData("AA_123_BB")]
        [InlineData("INVALID-PLATE-999")]
        public void Ctor_WhenFormatIsInvalid_ShouldThrowDomainException(string value)
        {
            Assert.Throws<DomainException>(() => new LicensePlate(value));
        }
    }
}
