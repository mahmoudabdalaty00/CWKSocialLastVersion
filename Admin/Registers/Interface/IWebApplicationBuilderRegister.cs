namespace Admin.Registers.Interface
{

    /// <summary>
    /// Implement this on any class that needs to register services
    /// (builder.Services.Add...) before the app is built.
    /// </summary>
    public interface IWebApplicationBuilderRegister : IRegister
    {
        void RegisterServices(WebApplicationBuilder builder);
    }

}
