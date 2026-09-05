using Admin.Registers.Interface;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Admin.Registers
{
    /// <summary>
    /// Registers cookie-based authentication for the admin panel, and adds
    /// the corresponding UseAuthentication/UseAuthorization middleware.
    /// </summary>
    public class AuthenticationRegister : IWebApplicationBuilderRegister, IWebApplicationRegister
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Account/AccessDenied";
                    options.ExpireTimeSpan = TimeSpan.FromHours(8);
                    options.SlidingExpiration = true;
                });

            builder.Services.AddAuthorization();
        }

        public void RegisterPipelineComponents(WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}
