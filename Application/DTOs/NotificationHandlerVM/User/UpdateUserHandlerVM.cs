using System.ComponentModel;
using MediatR;

namespace Application.DTOs.NotificationHandlerVM.User;

public class UpdateUserHandlerVM : INotification
{
    //public ApplicationUser User { get; set; }

    [DefaultValue(false)]
    public bool AddRole { get; set; }
}
