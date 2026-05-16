using Data.MainDb;
using Microsoft.EntityFrameworkCore;

namespace API.Registers
{
    public class DbRegister : IWebApplicationBuilderRegister
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            var cs = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<DataContext>(options =>
            {
                options.UseNpgsql(cs);
            });
        }
    }
}
