namespace Application.DTOs.Responses;

public class AdminResponse
{
    public bool Succeeded { get; set; }
    public string Msg { get; set; }
    public IEnumerable<ErrorRequestViewModel> Errors { get; set; }
}
