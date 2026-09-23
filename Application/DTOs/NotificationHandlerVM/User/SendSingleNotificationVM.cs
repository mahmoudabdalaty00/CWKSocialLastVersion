using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.NotificationHandlerVM.User
{
    public class SendSingleNotificationVM
    {
        public string UserId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string? Email { get; set; }
        public string? UserToken { get; set; }
        public string? Langauge { get; set; }
    }
}
