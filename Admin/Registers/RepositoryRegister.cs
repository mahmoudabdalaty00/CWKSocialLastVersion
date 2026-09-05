using Admin.Registers.Interface;
using Application.Service.Implementation.Common;
using Application.Service.Interface.Common; 
namespace Admin.Registers
{
    public class RepositoryRegister : IWebApplicationBuilderRegister
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }


}
