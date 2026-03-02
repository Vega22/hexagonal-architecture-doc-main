using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Specs
{
    /// <summary>
    /// Host-level tests for customers endpoint model binding.
    /// </summary>
    public sealed class CustomersControllerModelValidationSpecs : Infrastructure.InfrastructureTestBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomersControllerModelValidationSpecs"/> class.
        /// </summary>
        /// <param name="fixture">Test fixture.</param>
        public CustomersControllerModelValidationSpecs(Infrastructure.GenericInfrastructureTestServerFixture fixture)
            : base(fixture)
        {
        }

        /// <summary>
        /// Should return bad request when fullName is missing.
        /// </summary>
        [Fact]
        public async Task PostCustomer_WhenFullNameIsMissing_ReturnsBadRequest()
        {
            using var client = Fixture.Server.CreateClient();
            var payload = new
            {
                documentNumber = "DOC-001",
                email = "john@doe.com",
                phone = "+34123456789",
            };

            var response = await client.PostAsJsonAsync("/api/customers", payload);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
