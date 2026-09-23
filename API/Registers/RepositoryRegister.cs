using Application.Service.Implementation.PostServices;
using Application.Service.Implementation.UserProfileServices;
using Application.Service.Interface.Services.PostServices;
using Application.Service.Interface.Services.UserProfileServices;
using Data.UnitOfWork;
using FluentValidation;

namespace API.Registers;

public class RepositoryRegister : IWebApplicationBuilderRegister
{
    public void RegisterServices(WebApplicationBuilder builder)
    {

        builder.Services.AddScoped<Data.UnitOfWork.UnitOfWork>();

        builder.Services.AddScoped<IUserProfileService, UserProfileService>();
        builder.Services.AddScoped<IPostService, PostService>();
        builder.Services.AddScoped<IPostInterActionService, PostInterActionService>();
        builder.Services.AddScoped<IPostCommentService, PostCommentService>();











        // Add FluentValidation - THIS IS THE MISSING PIECE
        builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

    }
}