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
                name: "post-comments",
                pattern: "admin/posts/{postId:int}/comments/{action=Index}/{id?}",
                defaults: new { controller = "PostComments" });

            app.MapControllerRoute(
                name: "post-interactions",
                pattern: "admin/posts/{postId:int}/interactions/{action=Index}/{id?}",
                defaults: new { controller = "PostInteractions" });

            app.MapControllerRoute(
                name: "profiles",
                pattern: "admin/profiles/{action=Index}/{id?}",
                defaults: new { controller = "UserProfiles" });

            app.MapControllerRoute(
                name: "posts",
                pattern: "admin/posts/{action=Index}/{id?}",
                defaults: new { controller = "Posts" });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
