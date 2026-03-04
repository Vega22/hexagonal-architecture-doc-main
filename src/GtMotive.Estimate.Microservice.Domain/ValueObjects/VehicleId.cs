using System;

namespace GtMotive.Estimate.Microservice.Domain.ValueObjects
{
    /// <summary>
    /// Vehicle identifier value object.
    /// </summary>
    public readonly record struct VehicleId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleId"/> struct.
        /// </summary>
        /// <param name="value">Underlying value.</param>
        public VehicleId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new DomainException("Vehicle id cannot be empty.");
            }

            Value = value;
        }

        /// <summary>
        /// Gets underlying value.
        /// </summary>
        public Guid Value { get; }

        /// <summary>
        /// Creates a new identifier.
        /// </summary>
        /// <returns>New vehicle id.</returns>
        public static VehicleId New() => new(Guid.NewGuid());

        /// <summary>
        /// Returns the id as text.
        /// </summary>
        /// <returns>Text representation.</returns>
        public override string ToString() => Value.ToString();
    }
}
