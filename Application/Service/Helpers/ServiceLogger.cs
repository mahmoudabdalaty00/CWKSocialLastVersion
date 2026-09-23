using Application.DTOs.NotificationHandlerVM.Logging;
using Application.Service.Interface.Base;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Service.Helpers
{
    public class ServiceLogger
    {
        public static void AttachServiceLogger(IService service, ILogger logger)
        {
            service.ServiceLogger += (s, PassedMessage) => logger.Log(LogLevel.Information, PassedMessage);
        }
        
        //public static void AttachServiceLoggerByMediator(IService service, IMediator _mediator, IHttpContextAccessor _httpContext, UserManager<ApplicationUser> _userManager, string? controller = null, string? action =null)
        //{
        //    service.ServiceLogger += (s, PassedMessage) =>
        //    {
              
        //        _mediator.Publish(new LogAddViewModel()
        //        {
        //            //ApplicationUserId = HelperMethods.GetUserId(_httpContext, _userManager),
        //            IpAddress = HelperMethods.GetIpAddress(_httpContext),
        //            Table = controller,
        //            Action = action  ,
        //            Details = PassedMessage,
        //        });
        //    };
        //}

    }
}
