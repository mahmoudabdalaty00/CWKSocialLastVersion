using MediatR;

namespace Application.DTOs.NotificationHandlerVM.User;

public class AddUserRoleHandlerVM : INotification
{
    public string UserId { get; set; }
}
