using System;

[assembly: CLSCompliant(false)]

namespace GtMotive.Estimate.Microservice.Host.Configuration
{
    internal sealed class AppSettings
    {
        public string JwtAuthority { get; set; }

        public string JwtAuthorityInternal { get; set; }

        public string JwtAuthorityPublic { get; set; }

        public string JwtAudience { get; set; }

        public string SwaggerClientId { get; set; }

        public string SwaggerClientSecret { get; set; }
    }
}
