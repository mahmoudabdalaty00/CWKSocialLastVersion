using Admin.Registers.Interface;

namespace Admin.Registers
{
    /// <summary>
    /// Configures the request pipeline pieces MVC needs: static files,
    /// routing, and the default controller route.
    /// </summary>
    public class MvcPipelineRegister : IWebApplicationRegister
    {
        public void RegisterPipelineComponents(WebApplication app)
        {
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

           
        }
    }
}
 
 