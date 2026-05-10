using API.Options;

namespace API.Registers
{
    public class SwaggerRegister : IWebApplicationBuilderRegister
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            // Links your custom ConfigurationSwaggerOptions class (the one we created earlier)
            // to the Swagger generation process. This ensures Swagger knows about V1, V2, etc.
            builder.Services.ConfigureOptions<ConfigurationSwaggerOptions>();

            // Registers the Swagger Generator service. 
            // Because of the line above, we don't need to pass options here manually.
            builder.Services.AddSwaggerGen();
        }
    }
}
