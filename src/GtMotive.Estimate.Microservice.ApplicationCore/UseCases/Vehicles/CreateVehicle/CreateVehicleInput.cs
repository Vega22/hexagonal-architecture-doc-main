using GtMotive.Estimate.Microservice.ApplicationCore.UseCases;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.Vehicles.CreateVehicle
{
    /// <summary>
    /// Input for create vehicle use case.
    /// </summary>
    public sealed class CreateVehicleInput : IUseCaseInput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVehicleInput"/> class.
        /// </summary>
        /// <param name="licensePlate">Vehicle plate.</param>
        /// <param name="manufactureDate">Manufacture date.</param>
        /// <param name="brand">Vehicle brand.</param>
        /// <param name="model">Vehicle model.</param>
        public CreateVehicleInput(
            LicensePlate licensePlate,
            ManufactureDate manufactureDate,
            string brand,
            string model)
        {
            LicensePlate = licensePlate;
            ManufactureDate = manufactureDate;
            Brand = brand;
            Model = model;
        }

        /// <summary>
        /// Gets plate.
        /// </summary>
        public LicensePlate LicensePlate { get; }

        /// <summary>
        /// Gets manufacture date.
        /// </summary>
        public ManufactureDate ManufactureDate { get; }

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
