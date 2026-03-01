using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Specs
{
    /// <summary>
    /// Host-level tests for rentals endpoint model binding.
    /// </summary>
    public sealed class RentalsControllerModelValidationSpecs : Infrastructure.InfrastructureTestBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RentalsControllerModelValidationSpecs"/> class.
        /// </summary>
        /// <param name="fixture">Test fixture.</param>
        public RentalsControllerModelValidationSpecs(Infrastructure.GenericInfrastructureTestServerFixture fixture)
            : base(fixture)
        {
        }

        /// <summary>
        /// Should return bad request when customerId is missing on update.
        /// </summary>
        [Fact]
        public async Task PutRental_WhenCustomerIdIsMissing_ReturnsBadRequest()
        {
            using var client = Fixture.Server.CreateClient();
            var payload = new
            {
                vehicleId = Guid.NewGuid(),
                startAtUtc = DateTime.UtcNow,
                endAtUtc = (DateTime?)null,
            };

            var response = await client.PutAsJsonAsync($"/api/rentals/{Guid.NewGuid()}", payload);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
