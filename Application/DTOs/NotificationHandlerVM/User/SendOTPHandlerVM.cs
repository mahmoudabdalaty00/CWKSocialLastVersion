using Application.DTOs.Responses;
using MediatR;

namespace Application.DTOs.NotificationHandlerVM.User;
public class SendOTPHandlerVM : IRequest<OTPResponseViewModel>
{
    public string Mobile { get; set; }
    public bool EnableOTP { get; set; }
    public string Language { get; set; }

}


