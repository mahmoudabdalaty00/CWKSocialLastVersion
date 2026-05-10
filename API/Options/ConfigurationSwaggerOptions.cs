using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Options
{
    /// <summary>
    /// Configures Swagger generation options dynamically based on the detected API versions.
    /// This prevents the need to manually add SwaggerDoc("v1", ...) for every new version.
    /// </summary>
    public class ConfigurationSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _apiProvider;

        // Dependency Injection: The provider gives us a list of all versions found in our controllers
        public ConfigurationSwaggerOptions(IApiVersionDescriptionProvider apiProvider)
        {
            _apiProvider = apiProvider;
        }

        /// <summary>
        /// This method runs for each SwaggerGenOptions instance.
        /// It loops through every version discovered by the API Explorer.
        /// </summary>
        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _apiProvider.ApiVersionDescriptions)
            {
                // GroupName will be "v1", "v2", etc., based on your GroupNameFormat setup in Program.cs
                options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
            }
        }

        /// <summary>
        /// Helper method to create the metadata (Title, Version, Description) for each Swagger page.
        /// </summary>
        private OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Title = "CWKSocial API",
                Version = description.ApiVersion.ToString(),
                Description = "Professional Social Media Platform API"
            };

            // If you mark an old version with [ApiVersion("1.0", Deprecated = true)], 
            // this warning will automatically appear in the Swagger UI.
            if (description.IsDeprecated)
            {
                info.Description += " (This API version has been deprecated. Please use a newer version.)";
            }

            return info;
        }
    }
}
 
