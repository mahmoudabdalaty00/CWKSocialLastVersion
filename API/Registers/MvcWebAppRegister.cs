using Asp.Versioning.ApiExplorer;

namespace API.Registers
{
    public class MvcWebAppRegister : IWebApplicationRegister
    {
        public void RegisterPipelineComponents(WebApplication app)
        {

             
            // Generates the actual swagger.json files at /swagger/v1/swagger.json
            app.UseSwagger();

            // Configures the interactive Swagger UI web page
            app.UseSwaggerUI(options =>
            {
                // We use the IApiVersionDescriptionProvider to dynamically find all 
                // versions defined in our ConfigurationSwaggerOptions class.
                var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    // This builds the dropdown in the top right of the UI.
                    // GroupName is usually "v1", "v2", etc.
                    options.SwaggerEndpoint(
                        $"/swagger/{description.GroupName}/swagger.json",
                        $"CWSocial API - {description.ApiVersion}"
                    );
                }
            });
       
            // Development-only tools (like the new .NET 8+ MapOpenApi)
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            // Security: Forces HTTP traffic to use HTTPS for privacy
            app.UseHttpsRedirection();

            // Security: Checks if the user is logged in/has permission
            // Note: UseAuthentication() usually goes BEFORE UseAuthorization()
            app.UseAuthorization();

            // Routing: Links incoming URL paths to your Controller Actions
            app.MapControllers();
        }
    }
}
