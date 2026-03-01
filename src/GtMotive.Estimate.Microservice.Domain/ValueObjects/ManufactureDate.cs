using System;
using GtMotive.Estimate.Microservice.Domain.Exceptions;

namespace GtMotive.Estimate.Microservice.Domain.ValueObjects
{
    /// <summary>
    /// Manufacture date value object.
    /// </summary>
    public readonly record struct ManufactureDate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManufactureDate"/> struct.
        /// </summary>
        /// <param name="value">Date value.</param>
        public ManufactureDate(DateOnly value)
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            if (value > today)
            {
                throw new DomainException("Manufacture date cannot be in the future.");
            }

            Value = value;
        }

        /// <summary>
        /// Gets underlying value.
        /// </summary>
        public DateOnly Value { get; }

        /// <summary>
        /// Validates fleet age policy.
        /// </summary>
        public void EnsureWithinFleetAgePolicy()
        {
            var today = DateOnly.FromDateTime(DateTime.UtcNow);
            if (Value < today.AddYears(-5))
            {
                throw new VehicleTooOldForFleetException(Value);
            }
        }

        /// <summary>
        /// Returns date as text.
        /// </summary>
        /// <returns>Date text.</returns>
        public override string ToString() => Value.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
    }
}
