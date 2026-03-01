using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.ViewModels
{
    /// <summary>
    /// Rental representation.
    /// </summary>
    public sealed class RentalModel
    {
        public RentalModel(
            Guid rentalId,
            Guid vehicleId,
            string customerId,
            DateTime startAtUtc,
            DateTime? endAtUtc,
            bool isActive)
        {
            RentalId = rentalId;
            VehicleId = vehicleId;
            CustomerId = customerId;
            StartAtUtc = startAtUtc;
            EndAtUtc = endAtUtc;
            IsActive = isActive;
        }

        [Required]
        public Guid RentalId { get; }

        [Required]
        public Guid VehicleId { get; }

        [Required]
        public string CustomerId { get; }

        [Required]
        public DateTime StartAtUtc { get; }

        public DateTime? EndAtUtc { get; }

        [Required]
        public bool IsActive { get; }
    }
}
