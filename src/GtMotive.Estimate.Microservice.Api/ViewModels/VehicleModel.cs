using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.ViewModels
{
    /// <summary>
    /// Vehicle representation.
    /// </summary>
    public sealed class VehicleModel
    {
        public VehicleModel(
            Guid vehicleId,
            string licensePlate,
            DateOnly manufactureDate,
            string brand,
            string model,
            bool isDeleted,
            DateTime? deletedAtUtc)
        {
            VehicleId = vehicleId;
            LicensePlate = licensePlate;
            ManufactureDate = manufactureDate;
            Brand = brand;
            Model = model;
            IsDeleted = isDeleted;
            DeletedAtUtc = deletedAtUtc;
        }

        [Required]
        public Guid VehicleId { get; }

        [Required]
        public string LicensePlate { get; }

        [Required]
        public DateOnly ManufactureDate { get; }

        [Required]
        public string Brand { get; }

        [Required]
        public string Model { get; }

        [Required]
        public bool IsDeleted { get; }

        public DateTime? DeletedAtUtc { get; }
    }
}
