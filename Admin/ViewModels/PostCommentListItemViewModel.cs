namespace Admin.ViewModels;

public sealed class PostCommentListItemViewModel
{
    public int Id { get; init; }
    public string PostId { get; init; }
    public string Text { get; init; } = string.Empty;
    public string CreatedById { get; init; }
    public string UpdatedById { get; init; }
    public DateTime CreatedAt { get; init; }
}
