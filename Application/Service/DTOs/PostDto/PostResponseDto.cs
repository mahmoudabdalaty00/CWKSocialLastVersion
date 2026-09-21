namespace Application.Service.DTOs.PostDto;

public class PostResponseDto
{
    public string Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public string CreatedById { get; set; } = string.Empty;
    public string UpdatedById { get; set; } = string.Empty;
    public string DeletedById { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}
