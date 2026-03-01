using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.ViewModels
{
    /// <summary>
    /// Response body for returning a vehicle.
    /// </summary>
    public sealed class ReturnVehicleResponseModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnVehicleResponseModel"/> class.
        /// </summary>
        public ReturnVehicleResponseModel(Guid vehicleId)
        {
            VehicleId = vehicleId;
        }

        /// <summary>
        /// Gets returned vehicle id.
        /// </summary>
        [Required]
        public Guid VehicleId { get; }
    }
}
