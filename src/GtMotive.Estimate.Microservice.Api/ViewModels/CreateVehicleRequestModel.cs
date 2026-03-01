using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.ViewModels
{
    /// <summary>
    /// Request body for creating a vehicle.
    /// </summary>
    public sealed class CreateVehicleRequestModel
    {
        /// <summary>
        /// Gets or sets vehicle license plate.
        /// </summary>
        [Required]
        public string LicensePlate { get; set; }

        /// <summary>
        /// Gets or sets manufacture date.
        /// </summary>
        [Required]
        public DateOnly ManufactureDate { get; set; }

        /// <summary>
        /// Gets or sets vehicle brand.
        /// </summary>
        [Required]
        public string Brand { get; set; }

        /// <summary>
        /// Gets or sets vehicle model.
        /// </summary>
        [Required]
        public string Model { get; set; }
    }
}
