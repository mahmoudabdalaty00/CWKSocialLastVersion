using Admin.Registers.Interface;

namespace Admin.Registers
{
    /// <summary>
    /// Registers MVC (controllers + views) services for the Admin project.
    /// </summary>
    public class MvcRegister : IWebApplicationBuilderRegister
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllersWithViews();
        }
    }








}

