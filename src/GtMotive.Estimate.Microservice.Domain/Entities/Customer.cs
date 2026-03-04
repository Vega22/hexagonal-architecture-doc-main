#nullable enable
using GtMotive.Estimate.Microservice.Domain;
using GtMotive.Estimate.Microservice.Domain.Events;
using GtMotive.Estimate.Microservice.Domain.Validation;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Aggregate root representing a customer.
    /// </summary>
    public sealed partial class Customer : AggregateRoot
    {
        private const string SupportedCountryCode = "ES";
        private const string DniType = "DNI";
        private const string NieType = "NIE";
        private const string CifType = "CIF";

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
        {
            var customer = new Customer(Guid.NewGuid(), fullName, countryCode, documentType, documentNumber, email, phone);
            customer.AddDomainEvent(
                new CustomerCreatedDomainEvent(
                    customer.Id,
                    customer.CountryCode,
                    customer.DocumentType,
                    customer.DocumentNumber));
            return customer;
        }

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
            AddDomainEvent(new CustomerUpdatedDomainEvent(Id, CountryCode, DocumentType, DocumentNumber));
        }

        /// <summary>
        /// Marks customer as soft deleted.
        /// </summary>
        public void MarkDeleted()
        {
            EnsureNotDeleted();
            IsDeleted = true;
            UpdatedAtUtc = DateTime.UtcNow;
            AddDomainEvent(new CustomerDeletedDomainEvent(Id, UpdatedAtUtc));
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
            if (normalized is not DniType and not NieType and not CifType)
            {
                throw new DomainException($"Document type '{normalized}' is invalid for Spain. Allowed values: {DniType}, {NieType}, {CifType}.");
            }

            return normalized;
        }

        private static string NormalizeDocumentNumber(string documentNumber, string countryCode, string documentType)
        {
            var normalized = EnsureText(documentNumber, nameof(documentNumber), 32)
                .Replace(" ", string.Empty, StringComparison.Ordinal)
                .Replace("-", string.Empty, StringComparison.Ordinal)
                .ToUpperInvariant();

            if (countryCode == SupportedCountryCode)
            {
                ValidateSpanishDocument(normalized, documentType);
            }

            return normalized;
        }

        private static void ValidateSpanishDocument(string documentNumber, string documentType)
        {
            if (!SpanishDocumentValidator.IsValid(documentNumber, documentType))
            {
                throw new DomainException($"Document number '{documentNumber}' is invalid for {SupportedCountryCode}-{documentType}.");
            }
        }

        private static string? NormalizeOptionalEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return null;
            }

#pragma warning disable CA1308 // Email normalization: lowercase is standard for addresses
            var normalized = email.Trim().ToLowerInvariant();
#pragma warning restore CA1308
            if (normalized.Length > 256)
            {
                throw new DomainException("Email length must be <= 256.");
            }

            if (!CustomerRegex.Email().IsMatch(normalized))
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
            if (!CustomerRegex.E164Phone().IsMatch(normalized))
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
