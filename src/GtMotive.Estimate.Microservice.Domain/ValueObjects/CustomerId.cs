namespace GtMotive.Estimate.Microservice.Domain.ValueObjects
{
    /// <summary>
    /// Customer identifier value object.
    /// </summary>
    public readonly record struct CustomerId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomerId"/> struct.
        /// </summary>
        /// <param name="value">Underlying value.</param>
        public CustomerId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new DomainException("Customer id cannot be empty.");
            }

            Value = value;
        }

        /// <summary>
        /// Gets underlying value.
        /// </summary>
        public Guid Value { get; }

        /// <summary>
        /// Returns the id as text.
        /// </summary>
        /// <returns>Text representation.</returns>
        public override string ToString() => Value.ToString();
    }
}
