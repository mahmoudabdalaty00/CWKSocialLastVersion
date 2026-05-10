namespace API.Registers
{
    public interface IWebApplicationBuilderRegister :IRegistrar
    {
        public void RegisterServices(WebApplicationBuilder builder);
    }
}
