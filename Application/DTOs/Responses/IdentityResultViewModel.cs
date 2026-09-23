namespace Application.DTOs.Responses;

public class IdentityResultViewModel
{
    public IEnumerable<ErrorRequestViewModel> Errors { get; set; }
    public bool Succeeded { get; set; }
    public int? ItemId { get; set; }
    public string UserId { get; set; }
    public string Msg { get; set; }
    //public UserReadDto Data { get; set; }
}
