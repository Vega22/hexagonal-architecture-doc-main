using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.CreateVehicle
{
    /// <summary>
    /// Output for create vehicle use case.
    /// </summary>
    public sealed class CreateVehicleOutput : IUseCaseOutput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVehicleOutput"/> class.
        /// </summary>
        public CreateVehicleOutput(
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
        /// Gets license plate.
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
