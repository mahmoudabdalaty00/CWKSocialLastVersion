using Domain.Models.Conasts;

namespace API.Contracts.Posts.Responses
{
    public class PostResponse
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public string? MediaUrl { get; set; }
        public PostType PostType { get; set; }   
        public PrivacySetting PrivacySetting { get; set; } 
        public Guid UserProfileId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
