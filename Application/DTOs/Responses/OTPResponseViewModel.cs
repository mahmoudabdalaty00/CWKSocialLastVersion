namespace Application.DTOs.Responses;

public class OTPResponseViewModel
{
    public string Message { get; set; }
    public string Code { get; set; }
    public bool Resend { get; set; }
    public bool Try { get; set; }
    public int? ResendLimit { get; set; }
    public int? TryLimit { get; set; }
}
