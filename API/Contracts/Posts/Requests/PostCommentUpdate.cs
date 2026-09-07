namespace API.Contracts.Posts.Requests
{
    public record PostCommentUpdate
    {
        public string Text { get; set; } = string.Empty;
    }
}
