using System;
using System.Collections.Generic;
using System.Reflection;
using GtMotive.Estimate.Microservice.Host.Configuration;
using GtMotive.Estimate.Microservice.Host.Infrastructure.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace GtMotive.Estimate.Microservice.Host.DependencyInjection
{
    internal static class SwaggerExtensions
    {
        private static string AssemblyName => Assembly.GetEntryAssembly().GetName().Name;

        private static string AssemblyVersion => Assembly.GetEntryAssembly().GetName().Version.ToString();

        private static string SwaggerClientId(AppSettings settings)
        {
            return string.IsNullOrWhiteSpace(settings.SwaggerClientId)
                ? "client-gtestimate-swagger"
                : settings.SwaggerClientId;
        }

        private static string SwaggerClientSecret(AppSettings settings)
        {
            return string.IsNullOrWhiteSpace(settings.SwaggerClientSecret)
                ? string.Empty
                : settings.SwaggerClientSecret;
        }

        public static IServiceCollection AddSwagger(
            this IServiceCollection services,
            AppSettings settings,
            IConfiguration configuration)
        {
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(
                options =>
                {
                    options.CustomSchemaIds(type => type.ToString());
                    options.MapType<DateOnly>(() => new OpenApiSchema
                    {
                        Type = "string",
                        Format = "date",
                        Example = new OpenApiString("2025-01-23"),
                    });
                    options.SwaggerDoc($"v{AssemblyVersion}", new OpenApiInfo
                    {
                        Title = $"{AssemblyName} API",
                        Version = $"v{AssemblyVersion}",
                    });

                    if (configuration.GetValue<string>("Swagger:EnableTryIt") == "Yes")
                    {
                        var authority = string.IsNullOrWhiteSpace(settings.JwtAuthorityPublic)
                            ? settings.JwtAuthority
                            : settings.JwtAuthorityPublic;

                        // Define the OAuth2.0 scheme that's in use (i.e. Implicit Flow)
                        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                        {
                            Type = SecuritySchemeType.OAuth2,
                            Name = "oauth2",
                            Flows = configuration.GetValue<string>("Swagger:AuthFlow") == "AuthorizationCode"
                                ? new OpenApiOAuthFlows
                                {
                                    AuthorizationCode = new OpenApiOAuthFlow
                                    {
                                        AuthorizationUrl = new Uri($"{authority}/protocol/openid-connect/auth"),
                                        Scopes = new Dictionary<string, string>
                                        {
                                            ["estimate-api"] = "estimate-api"
                                        },
                                        TokenUrl = new Uri($"{authority}/protocol/openid-connect/token")
                                    }
                                }
                                : new OpenApiOAuthFlows()
                                {
                                    ClientCredentials = new OpenApiOAuthFlow()
                                    {
                                        Scopes = new Dictionary<string, string>
                                        {
                                            ["estimate-api"] = "estimate-api"
                                        },
                                        TokenUrl = new Uri($"{authority}/protocol/openid-connect/token")
                                    }
                                }
                        });

                        options.OperationFilter<IdentityServerApiSecurityOperationFilter>();
                    }
                });

            return services;
        }

        public static IApplicationBuilder UseSwaggerInApplication(
            this IApplicationBuilder app,
            PathBase pathBase,
            IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(pathBase);
            var settings = configuration.GetSection("AppSettings").Get<AppSettings>() ?? new AppSettings();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(options =>
            {
                if (!pathBase.IsDefault)
                {
                    options.SerializeAsV2 = true;
                    options.RouteTemplate = "swagger/{documentName}/swagger.json";
                    options.PreSerializeFilters.Add((document, request) =>
                    {
                        document.Servers =
                        [
                            new OpenApiServer
                            {
                                Url = $"{request.Scheme}://{request.Host.Value}{pathBase.CurrentWithoutTrailingSlash}"
                            }

                        ];
                    });
                }
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            var url = pathBase.IsDefault
                ? $"/swagger/v{AssemblyVersion}/swagger.json"
                : $"{pathBase.CurrentWithoutTrailingSlash}/swagger/v{AssemblyVersion}/swagger.json";

            app.UseSwaggerUI(
                options =>
                {
                    options.SwaggerEndpoint(url, $"{AssemblyName} API V{AssemblyVersion}");

                    if (configuration.GetValue<string>("Swagger:EnableTryIt") == "No")
                    {
                        options.SupportedSubmitMethods();
                    }

                    options.OAuthClientId(SwaggerClientId(settings));
                    var clientSecret = SwaggerClientSecret(settings);
                    if (!string.IsNullOrWhiteSpace(clientSecret))
                    {
                        options.OAuthClientSecret(clientSecret);
                    }
                    options.OAuthScopeSeparator(" ");
                    options.OAuthUsePkce();
                });

            return app;
        }
    }
}
