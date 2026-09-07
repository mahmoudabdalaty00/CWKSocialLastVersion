namespace API.Contracts.Posts.Requests
{
    public record PostCommentCreate
    {
        public Guid PostId { get; set; }
        public string Text { get; set; } = string.Empty;
        public Guid UserProfileId { get; set; }
    }
}
