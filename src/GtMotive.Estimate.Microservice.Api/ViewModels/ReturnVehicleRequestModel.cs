using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.ViewModels
{
    /// <summary>
    /// Request body for returning a vehicle.
    /// </summary>
    public sealed class ReturnVehicleRequestModel
    {
        /// <summary>
        /// Gets or sets vehicle id.
        /// </summary>
        [Required]
        public Guid VehicleId { get; set; }
    }
}
