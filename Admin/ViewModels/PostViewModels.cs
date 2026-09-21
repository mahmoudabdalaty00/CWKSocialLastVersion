using Domain.Models.Conasts;
using System.ComponentModel.DataAnnotations;

namespace Admin.ViewModels;

public class PostListItemViewModel
{
    public string Id { get; init; }
    public string Content { get; init; } = string.Empty;
    public string CreatedById { get; init; }
    public string UpdatedById { get; init; }
    public string DeletedById { get; init; }
    public DateTime CreatedAt { get; init; }
}

public sealed class PostDetailsViewModel : PostListItemViewModel
{
    public DateTime? UpdatedAt { get; init; }
}

public sealed class PostFormViewModel
{
    [Required, StringLength(2000)] public string Content { get; set; } = string.Empty;
    [Url, Display(Name = "Media URL")] public string MediaUrl { get; set; } = string.Empty;
    [Display(Name = "Post type")] public PostType PostType { get; set; } = PostType.Text;
    [Display(Name = "Privacy")] public PrivacySetting PrivacySetting { get; set; } = PrivacySetting.Public;
    [Required, Display(Name = "User profile ID")]
    public string? CreatedById { get; set; }
    public string? UpdatedById { get; set; }


}
