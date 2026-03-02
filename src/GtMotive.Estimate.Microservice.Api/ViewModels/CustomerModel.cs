#nullable enable
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.ViewModels
{
    public sealed class CustomerModel
    {
        public CustomerModel(
            Guid customerId,
            string fullName,
            string countryCode,
            string documentType,
            string documentNumber,
            string? email,
            string? phone,
            bool isDeleted,
            DateTime createdAtUtc,
            DateTime updatedAtUtc)
        {
            CustomerId = customerId;
            FullName = fullName;
            CountryCode = countryCode;
            DocumentType = documentType;
            DocumentNumber = documentNumber;
            Email = email;
            Phone = phone;
            IsDeleted = isDeleted;
            CreatedAtUtc = createdAtUtc;
            UpdatedAtUtc = updatedAtUtc;
        }

        [Required]
        public Guid CustomerId { get; }

        [Required]
        public string FullName { get; }

        [Required]
        public string CountryCode { get; }

        [Required]
        public string DocumentType { get; }

        [Required]
        public string DocumentNumber { get; }

        public string? Email { get; }

        public string? Phone { get; }

        [Required]
        public bool IsDeleted { get; }

        [Required]
        public DateTime CreatedAtUtc { get; }

        [Required]
        public DateTime UpdatedAtUtc { get; }
    }
}
