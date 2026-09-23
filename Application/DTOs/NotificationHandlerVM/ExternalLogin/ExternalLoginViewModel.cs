using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.NotificationHandlerVM.ExternalLogin;

internal class ExternalLoginViewModel
{
    [Required]
    public string IdToken { get; set; }

    [Display(Name = "Full Name")]
    public string? FullName { get; set; }

    [Display(Name = "Email")]
    public string Email { get; set; }
    public string? MobileAppId { get; set; }
    //public DeviceType? DeviceType { get; set; }
    public string? GoogleAccessToken { get; set; }
}
public class CheckGoogleVM
{
    public string IdToken { get; set; }
    public string Provider { get; set; }
}
