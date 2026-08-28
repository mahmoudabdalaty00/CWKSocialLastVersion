using API.Filters;
using Application.Features.UserProfiles.Validations;
using FluentValidation;
namespace API.Registers
{
    public partial class MvcRegister : IWebApplicationBuilderRegister
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            // Standard service to enable Web API Controllers
            builder.Services.AddControllers(config => {
                config.Filters.Add(typeof(CWKSocialExceptionHandler));
            });

            builder.Services.AddValidatorsFromAssemblyContaining<CreateUserProfileCommandValidator>();

        }
    }
}
