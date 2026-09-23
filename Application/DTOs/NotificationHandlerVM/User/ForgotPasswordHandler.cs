using MediatR;

namespace Application.DTOs.NotificationHandlerVM.User;

public class ForgotPasswordHandlerVM : INotification
{
    //public ApplicationUser User { get; set; }
    public string Email { get; set; }
    public string language { get; set; }

}
