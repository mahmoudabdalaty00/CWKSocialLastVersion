using System.ComponentModel.DataAnnotations;

namespace Admin.ViewModels;

public sealed class PostCommentFormViewModel
{
    [Required] public int? PostId { get; set; }
    [Required, StringLength(1000), DataType(DataType.MultilineText)] public string Text { get; set; } = string.Empty;
    [Required, Display(Name = "User profile ID")] public Guid? UserProfileId { get; set; }
}
