namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.ListAvailableVehicles
{
    /// <summary>
    /// Available vehicle output item.
    /// </summary>
    public sealed class AvailableVehicleItemOutput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AvailableVehicleItemOutput"/> class.
        /// </summary>
        public AvailableVehicleItemOutput(
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
        public Guid VehicleId { get; }

        /// <summary>
        /// Gets plate.
        /// </summary>
        public string LicensePlate { get; }

        /// <summary>
        /// Gets manufacture date.
        /// </summary>
        public DateOnly ManufactureDate { get; }

        /// <summary>
        /// Gets brand.
        /// </summary>
        public string Brand { get; }

        /// <summary>
        /// Gets model.
        /// </summary>
        public string Model { get; }
    }
}
