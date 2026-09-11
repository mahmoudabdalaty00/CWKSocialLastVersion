using Domain.Models.Conasts;
using System.ComponentModel.DataAnnotations;

namespace Admin.ViewModels;

public sealed class PostInteractionFormViewModel
{
    [Required] public int? PostId { get; set; }
    [Required, Display(Name = "Reaction")] public ReactionType? ReactionType { get; set; }
}
