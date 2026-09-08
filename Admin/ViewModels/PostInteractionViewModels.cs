using Domain.Models.Conasts;
using System.ComponentModel.DataAnnotations;

namespace Admin.ViewModels;

public sealed class PostInteractionListItemViewModel
{
    public int Id { get; init; }
    public int PostId { get; init; }
    public ReactionType ReactionType { get; init; }
    public DateTime CreatedAt { get; init; }
}

public sealed class PostInteractionFormViewModel
{
    [Required] public int? PostId { get; set; }
    [Required, Display(Name = "Reaction")] public ReactionType? ReactionType { get; set; }
}
