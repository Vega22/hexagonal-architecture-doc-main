using GtMotive.Estimate.Microservice.Api.UseCases;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Rentals
{
    /// <summary>
    /// MediatR request for renting a vehicle.
    /// </summary>
    public sealed class RentVehicleRequest : IRequest<IWebApiPresenter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleRequest"/> class.
        /// </summary>
        public RentVehicleRequest(Guid vehicleId, string customerId, DateTime? reservedFromUtc = null)
        {
            VehicleId = vehicleId;
            CustomerId = customerId;
            ReservedFromUtc = reservedFromUtc;
        }

        /// <summary>
        /// Gets vehicle id.
        /// </summary>
        public Guid VehicleId { get; }

        /// <summary>
        /// Gets customer id.
        /// </summary>
        public string CustomerId { get; }

        /// <summary>
        /// Gets requested reservation start date.
        /// </summary>
        public DateTime? ReservedFromUtc { get; }
    }
}
