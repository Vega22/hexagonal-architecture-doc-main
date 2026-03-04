using System.Text.RegularExpressions;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Generated regex for <see cref="Customer"/> validation.
    /// </summary>
    public static partial class CustomerRegex
    {
        [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase)]
        public static partial Regex Email();

        [GeneratedRegex(@"^\+[1-9]\d{7,14}$", RegexOptions.CultureInvariant)]
        public static partial Regex E164Phone();
    }
}
