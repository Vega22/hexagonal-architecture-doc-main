using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.ViewModels
{
    /// <summary>
    /// Response body for creating a vehicle.
    /// </summary>
    public sealed class CreateVehicleResponseModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVehicleResponseModel"/> class.
        /// </summary>
        public CreateVehicleResponseModel(
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
        /// Gets vehicle plate.
        /// </summary>
        [Required]
        public string LicensePlate { get; }

        /// <summary>
        /// Gets manufacture date.
        /// </summary>
        [Required]
        public DateOnly ManufactureDate { get; }

        /// <summary>
        /// Gets vehicle brand.
        /// </summary>
        [Required]
        public string Brand { get; }

        /// <summary>
        /// Gets vehicle model.
        /// </summary>
        [Required]
        public string Model { get; }
    }
}
