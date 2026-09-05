namespace Application.Service.DTOs.PostCommentDto;

public class CreatePostCommentDto
{
    public int PostId { get; set; }
    public string Text { get; set; } = string.Empty;
    public Guid UserProfileId { get; set; }
}
