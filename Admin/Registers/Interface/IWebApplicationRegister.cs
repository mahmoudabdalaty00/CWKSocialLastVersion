namespace Admin.Registers.Interface
{
    /// <summary>
    /// Implement this on any class that needs to configure the middleware
    /// pipeline (app.Use..., app.Map...) after the app is built.
    /// </summary>
    public interface IWebApplicationRegister : IRegister
    {
        void RegisterPipelineComponents(WebApplication app);
    }

}
