using Domain.Models.Conasts;

namespace API.Contracts.Posts.Requests
{
    public record PostInteractionUpdate
    {
        public ReactionType ReactionType { get; set; }
    }
}
