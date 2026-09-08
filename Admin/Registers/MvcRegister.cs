using Admin.Registers.Interface;

using Application.Features.UserProfiles.Validations;
using FluentValidation;

namespace Admin.Registers
{
    /// <summary>
    /// Registers MVC (controllers + views) services for the Admin project.
    /// </summary>
    public class MvcRegister : IWebApplicationBuilderRegister
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddControllersWithViews();
            builder.Services.AddValidatorsFromAssemblyContaining<CreateUserProfileDtoValidator>();
        }
    }








}

