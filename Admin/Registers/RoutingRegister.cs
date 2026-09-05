using Admin.Registers.Interface;

namespace Admin.Registers
{
    /// <summary>
    /// Registers the default MVC controller route.
    /// Must run after routing has been enabled (see MvcPipelineRegister's
    /// app.UseRouting() call) — see the ordering note where this is used.
    /// </summary>
    public class RoutingRegister : IWebApplicationRegister
    {
        public void RegisterPipelineComponents(WebApplication app)
        {
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
