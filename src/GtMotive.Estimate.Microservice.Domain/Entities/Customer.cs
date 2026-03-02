#nullable enable
using System.Text.RegularExpressions;
using GtMotive.Estimate.Microservice.Domain;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Aggregate root representing a customer.
    /// </summary>
    public sealed class Customer
    {
        private const string SupportedCountryCode = "ES";
        private const string DniType = "DNI";
        private const string NieType = "NIE";
        private const string CifType = "CIF";
        private const string DniLetters = "TRWAGMYFPDXBNJZSQVHLCKE";

        private static readonly Regex EmailRegex = new(
            @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

        private static readonly Regex E164PhoneRegex = new(
            @"^\+[1-9]\d{7,14}$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

        private static readonly Regex DniRegex = new(
            @"^\d{8}[A-Z]$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

        private static readonly Regex NieRegex = new(
            @"^[XYZ]\d{7}[A-Z]$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

        private static readonly Regex CifRegex = new(
            @"^[ABCDEFGHJKLMNPQRSUVW]\d{7}[0-9A-J]$",
            RegexOptions.Compiled | RegexOptions.CultureInvariant);

        private Customer()
        {
            FullName = string.Empty;
            CountryCode = string.Empty;
            DocumentType = string.Empty;
            DocumentNumber = string.Empty;
            Email = null;
            Phone = null;
        }

        private Customer(
            Guid id,
            string fullName,
            string countryCode,
            string documentType,
            string documentNumber,
            string? email,
            string? phone)
        {
            Id = id;
            FullName = EnsureText(fullName, nameof(fullName), 128);
            CountryCode = NormalizeCountryCode(countryCode);
            DocumentType = NormalizeDocumentType(documentType);
            DocumentNumber = NormalizeDocumentNumber(documentNumber, CountryCode, DocumentType);
            Email = NormalizeOptionalEmail(email);
            Phone = NormalizeOptionalPhone(phone);
            IsDeleted = false;
            CreatedAtUtc = DateTime.UtcNow;
            UpdatedAtUtc = CreatedAtUtc;
        }

        /// <summary>
        /// Gets customer id.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets full name.
        /// </summary>
        public string FullName { get; private set; }

        /// <summary>
        /// Gets country code (ISO-2).
        /// </summary>
        public string CountryCode { get; private set; }

        /// <summary>
        /// Gets document type.
        /// </summary>
        public string DocumentType { get; private set; }

        /// <summary>
        /// Gets document number.
        /// </summary>
        public string DocumentNumber { get; private set; }

        /// <summary>
        /// Gets email.
        /// </summary>
        public string? Email { get; private set; }

        /// <summary>
        /// Gets phone.
        /// </summary>
        public string? Phone { get; private set; }

        /// <summary>
        /// Gets a value indicating whether customer is soft deleted.
        /// </summary>
        public bool IsDeleted { get; private set; }

        /// <summary>
        /// Gets creation timestamp in UTC.
        /// </summary>
        public DateTime CreatedAtUtc { get; private set; }

        /// <summary>
        /// Gets last update timestamp in UTC.
        /// </summary>
        public DateTime UpdatedAtUtc { get; private set; }

        /// <summary>
        /// Creates a new customer aggregate.
        /// </summary>
        public static Customer Create(
            string fullName,
            string countryCode,
            string documentType,
            string documentNumber,
            string? email,
            string? phone)
            => new(Guid.NewGuid(), fullName, countryCode, documentType, documentNumber, email, phone);

        /// <summary>
        /// Updates mutable customer fields.
        /// </summary>
        public void Update(
            string fullName,
            string countryCode,
            string documentType,
            string documentNumber,
            string? email,
            string? phone)
        {
            EnsureNotDeleted();
            FullName = EnsureText(fullName, nameof(fullName), 128);
            CountryCode = NormalizeCountryCode(countryCode);
            DocumentType = NormalizeDocumentType(documentType);
            DocumentNumber = NormalizeDocumentNumber(documentNumber, CountryCode, DocumentType);
            Email = NormalizeOptionalEmail(email);
            Phone = NormalizeOptionalPhone(phone);
            UpdatedAtUtc = DateTime.UtcNow;
        }

        /// <summary>
        /// Marks customer as soft deleted.
        /// </summary>
        public void MarkDeleted()
        {
            EnsureNotDeleted();
            IsDeleted = true;
            UpdatedAtUtc = DateTime.UtcNow;
        }

        private static string EnsureText(string value, string fieldName, int maxLength)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new DomainException($"The field '{fieldName}' is required.");
            }

            var normalized = value.Trim();
            if (normalized.Length > maxLength)
            {
                throw new DomainException($"The field '{fieldName}' length must be <= {maxLength}.");
            }

            return normalized;
        }

        private static string NormalizeCountryCode(string countryCode)
        {
            var normalized = EnsureText(countryCode, nameof(countryCode), 2).ToUpperInvariant();
            if (normalized != SupportedCountryCode)
            {
                throw new DomainException($"Country '{normalized}' is not supported yet. This test currently supports '{SupportedCountryCode}' only.");
            }

            return normalized;
        }

        private static string NormalizeDocumentType(string documentType)
        {
            var normalized = EnsureText(documentType, nameof(documentType), 16).ToUpperInvariant();
            if (normalized != DniType && normalized != NieType && normalized != CifType)
            {
                throw new DomainException($"Document type '{normalized}' is invalid for Spain. Allowed values: {DniType}, {NieType}, {CifType}.");
            }

            return normalized;
        }

        private static string NormalizeDocumentNumber(string documentNumber, string countryCode, string documentType)
        {
            var normalized = EnsureText(documentNumber, nameof(documentNumber), 32)
                .Replace(" ", string.Empty)
                .Replace("-", string.Empty)
                .ToUpperInvariant();

            if (countryCode == SupportedCountryCode)
            {
                ValidateSpanishDocument(normalized, documentType);
            }

            return normalized;
        }

        private static void ValidateSpanishDocument(string documentNumber, string documentType)
        {
            var isValid = documentType switch
            {
                DniType => IsValidDni(documentNumber),
                NieType => IsValidNie(documentNumber),
                CifType => IsValidCif(documentNumber),
                _ => false,
            };

            if (!isValid)
            {
                throw new DomainException($"Document number '{documentNumber}' is invalid for {SupportedCountryCode}-{documentType}.");
            }
        }

        private static bool IsValidDni(string documentNumber)
        {
            if (!DniRegex.IsMatch(documentNumber))
            {
                return false;
            }

            var digits = int.Parse(documentNumber[..8]);
            var expected = DniLetters[digits % 23];
            return documentNumber[8] == expected;
        }

        private static bool IsValidNie(string documentNumber)
        {
            if (!NieRegex.IsMatch(documentNumber))
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

            var numeric = int.Parse($"{leading}{documentNumber.Substring(1, 7)}");
            var expected = DniLetters[numeric % 23];
            return documentNumber[8] == expected;
        }

        private static bool IsValidCif(string documentNumber)
        {
            if (!CifRegex.IsMatch(documentNumber))
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

            if ("PQSKW".Contains(firstLetter))
            {
                return control == controlLetter;
            }

            if ("ABEH".Contains(firstLetter))
            {
                return control == controlDigitChar;
            }

            return control == controlDigitChar || control == controlLetter;
        }

        private static string? NormalizeOptionalEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return null;
            }

            var normalized = email.Trim().ToLowerInvariant();
            if (normalized.Length > 256)
            {
                throw new DomainException("Email length must be <= 256.");
            }

            if (!EmailRegex.IsMatch(normalized))
            {
                throw new DomainException("Email format is invalid.");
            }

            return normalized;
        }

        private static string? NormalizeOptionalPhone(string? phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                return null;
            }

            var normalized = phone.Trim();
            if (!E164PhoneRegex.IsMatch(normalized))
            {
                throw new DomainException("Phone format is invalid. Use E.164 format, e.g. +34123456789.");
            }

            return normalized;
        }

        private void EnsureNotDeleted()
        {
            if (IsDeleted)
            {
                throw new DomainException($"Customer '{Id}' is deleted.");
            }
        }
    }
}
