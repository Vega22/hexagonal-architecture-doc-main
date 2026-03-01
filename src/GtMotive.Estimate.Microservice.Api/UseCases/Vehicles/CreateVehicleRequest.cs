using GtMotive.Estimate.Microservice.Api.UseCases;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.Vehicles
{
    /// <summary>
    /// MediatR request for create vehicle endpoint.
    /// </summary>
    public sealed class CreateVehicleRequest : IRequest<IWebApiPresenter>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVehicleRequest"/> class.
        /// </summary>
        public CreateVehicleRequest(
            string licensePlate,
            DateOnly manufactureDate,
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
