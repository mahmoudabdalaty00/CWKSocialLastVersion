using Domain.Models.Conasts;

namespace API.Contracts.Posts.Responses
{
    public class PostInteractionResponse
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public string ReactionType { get; set; } = string.Empty;
        public Guid UserProfileId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
