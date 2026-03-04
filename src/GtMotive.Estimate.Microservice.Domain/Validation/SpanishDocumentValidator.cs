using System.Globalization;
using System.Text.RegularExpressions;

namespace GtMotive.Estimate.Microservice.Domain.Validation
{
    /// <summary>
    /// Validates Spanish document numbers for DNI/NIE/CIF.
    /// </summary>
    public static partial class SpanishDocumentValidator
    {
        private const string DniLetters = "TRWAGMYFPDXBNJZSQVHLCKE";

        /// <summary>
        /// Returns true when the number is valid for the provided type.
        /// </summary>
        /// <param name="documentNumber">Normalized document number.</param>
        /// <param name="documentType">Normalized type (DNI, NIE or CIF).</param>
        /// <returns>Validation result.</returns>
        public static bool IsValid(string documentNumber, string documentType)
        {
            ArgumentNullException.ThrowIfNull(documentNumber);
            ArgumentNullException.ThrowIfNull(documentType);
            return documentType switch
            {
                "DNI" => IsValidDni(documentNumber),
                "NIE" => IsValidNie(documentNumber),
                "CIF" => IsValidCif(documentNumber),
                _ => false,
            };
        }

        private static bool IsValidDni(string documentNumber)
        {
            if (!DniRegex().IsMatch(documentNumber))
            {
                return false;
            }

            var digits = int.Parse(documentNumber[..8], CultureInfo.InvariantCulture);
            var expected = DniLetters[digits % 23];
            return documentNumber[8] == expected;
        }

        private static bool IsValidNie(string documentNumber)
        {
            if (!NieRegex().IsMatch(documentNumber))
            {
                return false;
            }

            var leading = documentNumber[0] switch
            {
                'X' => '0',
                'Y' => '1',
                'Z' => '2',
                _ => '9',
            };

            var numeric = int.Parse($"{leading}{documentNumber.Substring(1, 7)}", CultureInfo.InvariantCulture);
            var expected = DniLetters[numeric % 23];
            return documentNumber[8] == expected;
        }

        private static bool IsValidCif(string documentNumber)
        {
            if (!CifRegex().IsMatch(documentNumber))
            {
                return false;
            }

            var firstLetter = documentNumber[0];
            var digits = documentNumber.Substring(1, 7);
            var control = documentNumber[8];
            var sumEven = 0;
            var sumOdd = 0;

            for (var i = 0; i < digits.Length; i++)
            {
                var value = digits[i] - '0';
                if ((i + 1) % 2 == 0)
                {
                    sumEven += value;
                    continue;
                }

                var doubled = value * 2;
                sumOdd += (doubled / 10) + (doubled % 10);
            }

            var total = sumEven + sumOdd;
            var controlDigit = (10 - (total % 10)) % 10;
            var controlDigitChar = (char)('0' + controlDigit);
            var controlLetter = "JABCDEFGHI"[controlDigit];

            if ("PQSKW".Contains(firstLetter, StringComparison.Ordinal))
            {
                return control == controlLetter;
            }

            if ("ABEH".Contains(firstLetter, StringComparison.Ordinal))
            {
                return control == controlDigitChar;
            }

            return control == controlDigitChar || control == controlLetter;
        }

        [GeneratedRegex(@"^\d{8}[A-Z]$", RegexOptions.CultureInvariant)]
        private static partial Regex DniRegex();

        [GeneratedRegex(@"^[XYZ]\d{7}[A-Z]$", RegexOptions.CultureInvariant)]
        private static partial Regex NieRegex();

        [GeneratedRegex(@"^[ABCDEFGHJKLMNPQRSUVW]\d{7}[0-9A-J]$", RegexOptions.CultureInvariant)]
        private static partial Regex CifRegex();
    }
}
