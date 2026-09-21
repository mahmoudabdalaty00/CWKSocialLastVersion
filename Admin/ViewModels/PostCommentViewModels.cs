using System.ComponentModel.DataAnnotations;

namespace Admin.ViewModels;

public sealed class PostCommentFormViewModel
{
    [Required]
    public string? PostId { get; set; }


    [Required, StringLength(1000), DataType(DataType.MultilineText)] 
    public string Text { get; set; } = string.Empty;

    [Required, Display(Name = "Created by")]
    public string? CreatedById { get; set; }

    [Required, Display(Name = "Updated by")]
    public string? UpdatedById { get; set; }


}
