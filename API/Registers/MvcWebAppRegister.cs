namespace API.Registers
{
    public class MvcWebAppRegister : IWebApplicationRegister
    {
        public void RegisterPipelineComponents(WebApplication app)
        {
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
