using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.ViewModels
{
    /// <summary>
    /// Request body to update rental.
    /// </summary>
    public sealed class UpdateRentalRequestModel
    {
        [Required]
        public Guid VehicleId { get; set; }

        [Required]
        public string CustomerId { get; set; }

        [Required]
        public DateTime StartAtUtc { get; set; }

        public DateTime? EndAtUtc { get; set; }
    }
}
