using Asp.Versioning;

namespace API.Registers
{
    public partial class MvcRegister
    {
        public class ApiVersionServiceRegister : IWebApplicationBuilderRegister
        {
            public void RegisterServices(WebApplicationBuilder builder)
            {
                // New .NET standard for OpenAPI (can coexist with Swagger, but often redundant)
                builder.Services.AddOpenApi();

                // --- API VERSIONING CONFIGURATION ---
                builder.Services.AddApiVersioning(config =>
                {
                    // If the user doesn't specify a version, default to 1.0
                    config.DefaultApiVersion = new ApiVersion(1, 0);

                    // Allow the app to run even if the version isn't in the URL
                    config.AssumeDefaultVersionWhenUnspecified = true;

                    // Adds 'api-supported-versions' and 'api-deprecated-versions' to the response headers
                    config.ReportApiVersions = true;

                    // Tells the app to look for the version in the URL: /api/v1/controller
                    config.ApiVersionReader = new UrlSegmentApiVersionReader();

                })
                // --- API EXPLORER CONFIGURATION ---
                // This bridges the gap between Versioning and Swagger
                .AddApiExplorer(options =>
                {
                    // VVV means 'major.minor.patch'. This creates the "v1", "v2" group names.
                    options.GroupNameFormat = "'v'VVV";

                    // Replaces the "{version}" placeholder in [Route("api/v{version:apiVersion}/...")]
                    // so that Swagger generates valid URLs like /api/v1/Post
                    options.SubstituteApiVersionInUrl = true;
                });
            }
        }
    }
}
