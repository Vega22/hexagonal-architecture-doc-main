using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.ViewModels
{
    /// <summary>
    /// Vehicle item for list response.
    /// </summary>
    public sealed class AvailableVehicleModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AvailableVehicleModel"/> class.
        /// </summary>
        public AvailableVehicleModel(
            Guid vehicleId,
            string licensePlate,
            DateOnly manufactureDate,
            string brand,
            string model)
        {
            VehicleId = vehicleId;
            LicensePlate = licensePlate;
            ManufactureDate = manufactureDate;
            Brand = brand;
            Model = model;
        }

        /// <summary>
        /// Gets vehicle id.
        /// </summary>
        [Required]
        public Guid VehicleId { get; }

        /// <summary>
        /// Gets license plate.
        /// </summary>
        [Required]
        public string LicensePlate { get; }

        /// <summary>
        /// Gets manufacture date.
        /// </summary>
        [Required]
        public DateOnly ManufactureDate { get; }

        /// <summary>
        /// Gets brand.
        /// </summary>
        [Required]
        public string Brand { get; }

        /// <summary>
        /// Gets model.
        /// </summary>
        [Required]
        public string Model { get; }
    }
}
