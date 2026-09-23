using MediatR;

namespace Application.DTOs.NotificationHandlerVM.User;

public class AddUserInterestsHandlerVM : INotification
{
    public string UserId { get; set; }
}
