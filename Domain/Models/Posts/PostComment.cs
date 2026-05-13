using Domain.Models.BaseEntities;
namespace Domain.Models.Posts
{
    public class PostComment : BaseEntity<int>
    {
        private PostComment()
        {

        }

        public int PostId { get; private set; }
        public string Text { get; private set; }
        public Guid UserProfileId { get; private set; }


        //Factyory method to create a new comment
        public static PostComment Create(int postId, string text, Guid userProfileId)
        {
            return new PostComment
            {
                PostId = postId,
                Text = text,
                UserProfileId = userProfileId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
        }



        public static PostComment Update(string text)
        {
            return new PostComment
            {
                Text = text.Trim()
            };
        }


    }
}
