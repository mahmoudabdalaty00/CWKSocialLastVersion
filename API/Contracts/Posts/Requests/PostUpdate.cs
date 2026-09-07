using Domain.Models.Conasts;

namespace API.Contracts.Posts.Requests
{
    public record PostUpdate
    {
        public string Content { get; set; } = string.Empty;
        public string MediaUrl { get; set; } = string.Empty;
        public PostType PostType { get; set; } = PostType.Text;
        public PrivacySetting PrivacySetting { get; set; } = PrivacySetting.Public;
    }
}
