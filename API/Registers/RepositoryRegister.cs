using Application.Service.Implementation.Common;
using Application.Service.Implementation.Services.UserProfileServices;
using Application.Service.Interface.Common;
using Application.Service.Interface.Services.UserProfileServices;

namespace API.Registers;

public class RepositoryRegister : IWebApplicationBuilderRegister
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IUserProfileService, UserProfileService>();

    }
}