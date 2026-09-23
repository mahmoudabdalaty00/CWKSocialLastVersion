using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Application.DTOs.NotificationHandlerVM.Logging;

public class LogAddViewModel : INotification {

    [Required(ErrorMessage = "Required")]
    [MaxLength(450, ErrorMessage = "Maximum characters is 255 character")]
    public string ApplicationUserId { get; set; }

    [MaxLength(255, ErrorMessage = "Maximum characters is 255 character")]
    public string IpAddress { get; set; }

    [Required(ErrorMessage = "Required")]
    [MaxLength(50, ErrorMessage = "Maximum characters is 50 character")]
    [Display(Name = "Action")]
    public string Action { get; set; }

    [Required(ErrorMessage = "Required")]
    [MaxLength(50, ErrorMessage = "Maximum characters is 50 character")]
    [Display(Name = "Table")]
    public string Table { get; set; }

    [Required(ErrorMessage = "Required")]
    [MaxLength(1000, ErrorMessage = "Maximum characters is 255 character")]
    [Display(Name = "Details")]
    public string Details { get; set; }

}


