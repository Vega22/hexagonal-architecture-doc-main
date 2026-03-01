#nullable enable
using System;
using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Aggregate root representing a fleet vehicle.
    /// </summary>
    public sealed class Vehicle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// Required by EF.
        /// </summary>
        private Vehicle()
        {
            Brand = string.Empty;
            Model = string.Empty;
        }

        private Vehicle(
            VehicleId id,
            LicensePlate licensePlate,
            ManufactureDate manufactureDate,
            string brand,
            string model)
        {
            Id = id;
            LicensePlate = licensePlate;
            ManufactureDate = manufactureDate;
            Brand = brand;
            Model = model;
            IsDeleted = false;
            DeletedAtUtc = null;
        }

        /// <summary>
        /// Gets vehicle id.
        /// </summary>
        public VehicleId Id { get; private set; }

        /// <summary>
        /// Gets license plate.
        /// </summary>
        public LicensePlate LicensePlate { get; private set; }

        /// <summary>
        /// Gets manufacture date.
        /// </summary>
        public ManufactureDate ManufactureDate { get; private set; }

        /// <summary>
        /// Gets vehicle brand.
        /// </summary>
        public string Brand { get; private set; }

        /// <summary>
        /// Gets vehicle model.
        /// </summary>
        public string Model { get; private set; }

        /// <summary>
        /// Gets a value indicating whether vehicle is soft deleted.
        /// </summary>
        public bool IsDeleted { get; private set; }

        /// <summary>
        /// Gets deletion timestamp in UTC.
        /// </summary>
        public DateTime? DeletedAtUtc { get; private set; }

        /// <summary>
        /// Creates a new vehicle applying domain validations.
        /// </summary>
        /// <param name="licensePlate">Vehicle plate.</param>
        /// <param name="manufactureDate">Vehicle manufacture date.</param>
        /// <param name="brand">Vehicle brand.</param>
        /// <param name="model">Vehicle model.</param>
        /// <returns>New vehicle aggregate.</returns>
        public static Vehicle Create(
            LicensePlate licensePlate,
            ManufactureDate manufactureDate,
            string brand,
            string model)
        {
            manufactureDate.EnsureWithinFleetAgePolicy();
            return new Vehicle(
                VehicleId.New(),
                licensePlate,
                manufactureDate,
                EnsureText(brand, nameof(brand)),
                EnsureText(model, nameof(model)));
        }

        /// <summary>
        /// Updates mutable vehicle fields.
        /// </summary>
        /// <param name="licensePlate">Vehicle plate.</param>
        /// <param name="manufactureDate">Vehicle manufacture date.</param>
        /// <param name="brand">Vehicle brand.</param>
        /// <param name="model">Vehicle model.</param>
        public void UpdateDetails(
            LicensePlate licensePlate,
            ManufactureDate manufactureDate,
            string brand,
            string model)
        {
            EnsureNotDeleted();
            manufactureDate.EnsureWithinFleetAgePolicy();

            LicensePlate = licensePlate;
            ManufactureDate = manufactureDate;
            Brand = EnsureText(brand, nameof(brand));
            Model = EnsureText(model, nameof(model));
        }

        /// <summary>
        /// Marks vehicle as deleted without physical removal.
        /// </summary>
        /// <param name="deletedAtUtc">Deletion timestamp in UTC.</param>
        public void MarkDeleted(DateTime deletedAtUtc)
        {
            EnsureNotDeleted();
            IsDeleted = true;
            DeletedAtUtc = deletedAtUtc;
        }

        /// <summary>
        /// Restores a soft-deleted vehicle.
        /// </summary>
        public void Restore()
        {
            IsDeleted = false;
            DeletedAtUtc = null;
        }

        private void EnsureNotDeleted()
        {
            if (IsDeleted)
            {
                throw new DomainException($"Vehicle '{Id}' is deleted.");
            }
        }

        private static string EnsureText(string value, string fieldName)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"The field '{fieldName}' is required.", fieldName);
            }

            return value.Trim();
        }
    }
}
