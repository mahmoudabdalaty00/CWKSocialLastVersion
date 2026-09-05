using Domain.Models.Conasts;

namespace Application.Service.DTOs.PostDto;

public class CreatePostDto
{
    public string Content { get; set; } = string.Empty;
    public string MediaUrl { get; set; } = string.Empty;
    public PostType PostType { get; set; } = PostType.Text;
    public PrivacySetting PrivacySetting { get; set; } = PrivacySetting.Public;
    public Guid UserProfileId { get; set; }
}
