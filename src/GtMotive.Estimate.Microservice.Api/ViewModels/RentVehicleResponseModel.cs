using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.ViewModels
{
    /// <summary>
    /// Response body for renting a vehicle.
    /// </summary>
    public sealed class RentVehicleResponseModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleResponseModel"/> class.
        /// </summary>
        public RentVehicleResponseModel(Guid vehicleId, Guid customerId, DateTime rentedAtUtc)
        {
            VehicleId = vehicleId;
            CustomerId = customerId;
            RentedAtUtc = rentedAtUtc;
        }

        /// <summary>
        /// Gets vehicle id.
        /// </summary>
        [Required]
        public Guid VehicleId { get; }

        /// <summary>
        /// Gets customer id.
        /// </summary>
        [Required]
        public Guid CustomerId { get; }

        /// <summary>
        /// Gets rental timestamp.
        /// </summary>
        [Required]
        public DateTime RentedAtUtc { get; }
    }
}
