using Application.Features.UserProfiles.Queries;

namespace API.Registers
{
    public class BogardRegister : IWebApplicationBuilderRegister
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(cfg => { }, typeof(GetAllUserProfilesQuery).Assembly, typeof(BogardRegister).Assembly);

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(GetAllUserProfilesQuery).Assembly);
            });
        }
    }
}
