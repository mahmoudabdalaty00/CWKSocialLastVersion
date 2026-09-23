using Domain.Models.Conasts;

namespace Application.DTOs.PostDto;

public class CreatePostDto
{
    public string Content { get; set; } = string.Empty;
    public string MediaUrl { get; set; } = string.Empty;
    public PostType PostType { get; set; } = PostType.Text;
    public PrivacySetting PrivacySetting { get; set; } = PrivacySetting.Public;
    public string? CreatedById { get; set; }
    public string? UpdatedById { get; set; }
    public string? DeletedById { get; set; }

}
