using System;
using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Rentals.RentVehicle
{
    /// <summary>
    /// Input for rent vehicle use case.
    /// </summary>
    public sealed class RentVehicleInput : IUseCaseInput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleInput"/> class.
        /// </summary>
        public RentVehicleInput(VehicleId vehicleId, CustomerId customerId)
        {
            VehicleId = vehicleId;
            CustomerId = customerId;
            ReservedFromUtc = DateTime.UtcNow;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleInput"/> class.
        /// </summary>
        public RentVehicleInput(VehicleId vehicleId, CustomerId customerId, DateTime reservedFromUtc)
        {
            VehicleId = vehicleId;
            CustomerId = customerId;
            ReservedFromUtc = reservedFromUtc;
        }

        /// <summary>
        /// Gets vehicle id.
        /// </summary>
        public VehicleId VehicleId { get; }

        /// <summary>
        /// Gets customer id.
        /// </summary>
        public CustomerId CustomerId { get; }

        /// <summary>
        /// Gets reservation start date.
        /// </summary>
        public DateTime ReservedFromUtc { get; }
    }
}
