using Application.UserProfiles.Queries;

namespace API.Registers
{
    public class BogardRegister : IWebApplicationBuilderRegister
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(typeof(GetAllUserProfilesQuery).Assembly);

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(GetAllUserProfilesQuery).Assembly);
            });
        }
    }
}
