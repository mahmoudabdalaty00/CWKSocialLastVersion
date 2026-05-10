namespace API.Registers
{
    public partial class MvcRegister : IWebApplicationBuilderRegister
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            // Standard service to enable Web API Controllers
            builder.Services.AddControllers();
        }
    }
}
