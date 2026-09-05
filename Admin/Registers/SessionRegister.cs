using Admin.Registers.Interface;

namespace Admin.Registers
{
    /// <summary>
    /// Registers distributed memory cache + session state, and adds the
    /// UseSession middleware. Common requirement for admin dashboards
    /// (e.g. storing selected tenant, temp UI state, etc.).
    /// </summary>
    public class SessionRegister : IWebApplicationBuilderRegister, IWebApplicationRegister
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }

        public void RegisterPipelineComponents(WebApplication app)
        {
            app.UseSession();
        }
    }
}
