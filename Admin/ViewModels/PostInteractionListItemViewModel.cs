using Domain.Models.Conasts;

namespace Admin.ViewModels;

public sealed class PostInteractionListItemViewModel
{
    public int Id { get; init; }
    public int PostId { get; init; }
    public ReactionType ReactionType { get; init; }
    public DateTime CreatedAt { get; init; }
}
