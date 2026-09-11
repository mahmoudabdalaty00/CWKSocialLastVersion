namespace API.Contracts.Posts.Responses
{
    public class PostResponse
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public string? MediaUrl { get; set; }
        public string PostType { get; set; } = string.Empty;
        public string PrivacySetting { get; set; } = string.Empty;
        public Guid UserProfileId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
