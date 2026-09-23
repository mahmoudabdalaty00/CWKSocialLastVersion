using Admin.Registers.Interface;
using Application.Service.Interface.Services.PostServices;
using Application.Service.Interface.Services.UserProfileServices;
using Data.UnitOfWork;
using Application.Service.Implementation.UserProfileServices;
using Application.Service.Implementation.PostServices;

namespace Admin.Registers
{
    public class RepositoryRegister : IWebApplicationBuilderRegister
    {
        public void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<UnitOfWork>();
            builder.Services.AddScoped<IUserProfileService, UserProfileService>();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<IPostInterActionService, PostInterActionService>();
            builder.Services.AddScoped<IPostCommentService, PostCommentService>();

        }
    }


}
