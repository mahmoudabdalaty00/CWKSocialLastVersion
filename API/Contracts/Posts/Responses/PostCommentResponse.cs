namespace API.Contracts.Posts.Responses
{
    public class PostCommentResponse
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string Text { get; set; } = string.Empty;
        public Guid UserProfileId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
