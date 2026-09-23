
using MediatR;

namespace Application.DTOs.NotificationHandlerVM.SNS;

public class SNSRegisterUserHendler : INotification
{
    public string UserId { get; set; }
    public string? MobileAppId { get; set; }
    //public DeviceType DeviceType { get; set; }
    //public UserType UserType { get; set; }
}
