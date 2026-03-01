using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Specs
{
    /// <summary>
    /// Host-level tests for vehicles endpoint model binding.
    /// </summary>
    public sealed class VehiclesControllerModelValidationSpecs : Infrastructure.InfrastructureTestBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehiclesControllerModelValidationSpecs"/> class.
        /// </summary>
        /// <param name="fixture">Test fixture.</param>
        public VehiclesControllerModelValidationSpecs(Infrastructure.GenericInfrastructureTestServerFixture fixture)
            : base(fixture)
        {
        }

        /// <summary>
        /// Should return bad request when required fields are missing.
        /// </summary>
        [Fact]
        public async Task PostVehicle_WhenLicensePlateIsMissing_ReturnsBadRequest()
        {
            using var client = Fixture.Server.CreateClient();
            var payload = new
            {
                manufactureDate = "2023-01-01",
                brand = "Toyota",
                model = "Corolla",
            };

            var response = await client.PostAsJsonAsync("/api/vehicles", payload);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
