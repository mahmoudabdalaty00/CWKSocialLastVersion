using Admin.Registers.Interface;
using Data.MainDb;
using Microsoft.EntityFrameworkCore;

namespace Admin.Registers;

public class DbRegister : IWebApplicationBuilderRegister
{
    public void RegisterServices(WebApplicationBuilder builder)
    {

        var cs = builder.Configuration.GetConnectionString("DefaultConnection");

        // 1. Register the Write Context
        builder.Services.AddDbContext<WriteDbContext>(options =>
        {
            options.UseNpgsql(cs);
        });

        // 2. Register the Read Context
        builder.Services.AddDbContext<ReadDBContext>(options =>
        {
            options.UseNpgsql(cs);
        });

        // Keep DataContext only if other parts of your app specifically use it
        builder.Services.AddDbContext<DataContext>(options =>
        {
            options.UseNpgsql(cs);
        });

        //var connectionString =
        //   builder.Configuration.GetConnectionString("DefaultConnection");

        //builder.Services.AddDbContext<DataContext>(options =>
        //{
        //    options.UseSqlServer(connectionString);
        //});
    }
}