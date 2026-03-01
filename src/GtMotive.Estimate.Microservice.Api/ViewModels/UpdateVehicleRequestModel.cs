using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.ViewModels
{
    /// <summary>
    /// Request body to update vehicle.
    /// </summary>
    public sealed class UpdateVehicleRequestModel
    {
        [Required]
        public string LicensePlate { get; set; }

        [Required]
        public DateOnly ManufactureDate { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }
    }
}
