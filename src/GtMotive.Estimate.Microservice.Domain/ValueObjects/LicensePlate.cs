using System.Text.RegularExpressions;

namespace GtMotive.Estimate.Microservice.Domain.ValueObjects
{
    /// <summary>
    /// Vehicle license plate value object.
    /// </summary>
    public readonly partial record struct LicensePlate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LicensePlate"/> struct.
        /// </summary>
        /// <param name="value">Plate value.</param>
        public LicensePlate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainException("License plate is required.");
            }

            var normalized = value.Trim().ToUpperInvariant();
            if (!AllowedCharactersRegex().IsMatch(normalized))
            {
                throw new DomainException("License plate contains invalid characters.");
            }

            if (!IsEuropeanPlateFormat(normalized))
            {
                throw new DomainException("License plate format is invalid for supported European formats.");
            }

            Value = normalized;
        }

        /// <summary>
        /// Gets normalized plate.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Returns plate as text.
        /// </summary>
        /// <returns>Plate value.</returns>
        public override string ToString() => Value;

        private static bool IsEuropeanPlateFormat(string value)
        {
            return
                SpainCurrentRegex().IsMatch(value) ||
                SpainLegacyRegex().IsMatch(value) ||
                FranceItalyRegex().IsMatch(value) ||
                GermanyRegex().IsMatch(value) ||
                UkRegex().IsMatch(value) ||
                PortugalRegex().IsMatch(value) ||
                BelgiumRegex().IsMatch(value) ||
                IrelandRegex().IsMatch(value);
        }

        [GeneratedRegex("^[A-Z0-9\\-\\s]{4,12}$", RegexOptions.CultureInvariant)]
        private static partial Regex AllowedCharactersRegex();

        // Spain current format (e.g. 1234ABC).
        [GeneratedRegex("^[0-9]{4}[A-Z]{3}$", RegexOptions.CultureInvariant)]
        private static partial Regex SpainCurrentRegex();

        // Spain legacy format (e.g. M-1234-AB, MA-1234-AZ).
        [GeneratedRegex("^[A-Z]{1,2}-?[0-9]{4}-?[A-Z]{1,2}$", RegexOptions.CultureInvariant)]
        private static partial Regex SpainLegacyRegex();

        // France and Italy modern format (e.g. AB-123-CD, AB123CD).
        [GeneratedRegex("^[A-Z]{2}-?[0-9]{3}-?[A-Z]{2}$", RegexOptions.CultureInvariant)]
        private static partial Regex FranceItalyRegex();

        // Germany format (e.g. B-AB 1234, M-A 99).
        [GeneratedRegex("^[A-Z]{1,3}-[A-Z]{1,2}\\s?[0-9]{1,4}$", RegexOptions.CultureInvariant)]
        private static partial Regex GermanyRegex();

        // UK format (e.g. AB12 CDE, AB12CDE).
        [GeneratedRegex("^[A-Z]{2}[0-9]{2}\\s?[A-Z]{3}$", RegexOptions.CultureInvariant)]
        private static partial Regex UkRegex();

        // Portugal formats (e.g. 12-AB-34, AB-12-34, 12-34-AB, AB-12-CD).
        [GeneratedRegex("^([0-9]{2}-[A-Z]{2}-[0-9]{2}|[A-Z]{2}-[0-9]{2}-[0-9]{2}|[0-9]{2}-[0-9]{2}-[A-Z]{2}|[A-Z]{2}-[0-9]{2}-[A-Z]{2})$", RegexOptions.CultureInvariant)]
        private static partial Regex PortugalRegex();

        // Belgium format (e.g. 1-ABC-123).
        [GeneratedRegex("^[0-9]-[A-Z]{3}-[0-9]{3}$", RegexOptions.CultureInvariant)]
        private static partial Regex BelgiumRegex();

        // Ireland format (e.g. 171-D-12345, 99-DL-12).
        [GeneratedRegex("^[0-9]{2,3}-[A-Z]{1,2}-[0-9]{1,5}$", RegexOptions.CultureInvariant)]
        private static partial Regex IrelandRegex();
    }
}
