using MediatR;

namespace Application.DTOs.NotificationHandlerVM.ExternalLogin;

public class DeleteUserByProviderVM : INotification
{


    public string UserId { get; set; }

    public string ControllerName { get; set; }

    public string ActionName { get; set; }

    public bool Callback { get; set; }

    public bool Revoke_From_Server { get; set; }

}


