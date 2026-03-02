using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.ViewModels
{
    /// <summary>
    /// Request body for renting a vehicle.
    /// </summary>
    public sealed class RentVehicleRequestModel
    {
        /// <summary>
        /// Gets or sets vehicle id.
        /// </summary>
        [Required]
        public Guid? VehicleId { get; set; }

        /// <summary>
        /// Gets or sets customer id.
        /// </summary>
        [Required]
        public Guid? CustomerId { get; set; }

        /// <summary>
        /// Gets or sets reservation start date in UTC.
        /// </summary>
        public DateTime? ReservedFromUtc { get; set; }
    }
}
