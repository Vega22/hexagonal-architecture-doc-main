#nullable enable
using System.ComponentModel.DataAnnotations;

namespace GtMotive.Estimate.Microservice.Api.ViewModels
{
    public sealed class UpdateCustomerRequestModel
    {
        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string CountryCode { get; set; } = "ES";

        [Required]
        public string DocumentType { get; set; } = "DNI";

        [Required]
        public string DocumentNumber { get; set; } = string.Empty;

        public string? Email { get; set; }

        public string? Phone { get; set; }
    }
}
