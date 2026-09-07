using Domain.Models.Conasts;

namespace API.Contracts.Posts.Requests
{
    public record PostInteractionCreate
    {
        public Guid PostId { get; set; }
        public ReactionType ReactionType { get; set; }
    }
}
