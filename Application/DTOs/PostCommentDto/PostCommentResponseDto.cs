namespace Application.DTOs.PostCommentDto;

public class PostCommentResponseDto
{
    public int Id { get; set; }
    public string PostId { get; set; }
    public string Text { get; set; } = string.Empty;
    public string? CreatedById { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public string UpdatedById { get; set; }
}
