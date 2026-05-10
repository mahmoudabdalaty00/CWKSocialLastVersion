namespace API.Registers
{
    public interface IWebApplicationRegister : IRegistrar
    {
        public void RegisterPipelineComponents(WebApplication app);
    }
}
