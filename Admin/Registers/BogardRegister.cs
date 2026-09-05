using Admin.Registers.Interface;
using Application.AutoMapper;
namespace Admin.Registers;

public class BogardRegister : IWebApplicationBuilderRegister
{
    public void RegisterServices(WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(cfg => { }, typeof(UserProfileProfile).Assembly, typeof(BogardRegister).Assembly);

        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(UserProfileProfile).Assembly);
        });
    }
}