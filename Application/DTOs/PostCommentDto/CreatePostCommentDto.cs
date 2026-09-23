namespace Application.DTOs.PostCommentDto;

public class CreatePostCommentDto
{
    public string PostId { get; set; }
    public string Text { get; set; } = string.Empty;
    public string CreatedById { get; set; }
    public string UpdatedById { get; set; }
}
