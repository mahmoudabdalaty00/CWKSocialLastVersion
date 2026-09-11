namespace Admin.ViewModels;

public sealed class PostCommentListItemViewModel
{
    public int Id { get; init; }
    public int PostId { get; init; }
    public string Text { get; init; } = string.Empty;
    public Guid UserProfileId { get; init; }
    public DateTime CreatedAt { get; init; }
}
