using Domain.Models.BaseEntities;
using Domain.Models.Conasts;
using UserProfile = Domain.Models.UserProfiles.UserProfile;
namespace Domain.Models.Posts
{
    public class Post : BaseEntity<int>
    {
        private readonly List<PostComment> _postComments = new List<PostComment>();
        private readonly List<PostInterAction> _postInterAction = new List<PostInterAction>();

        private Post()
        {

        }

        public string Content { get; private set; } = string.Empty;

        public Guid UserProfileId { get; private set; }
        public UserProfile UserProfile { get; private set; }
        public IEnumerable<PostComment>? PostComments { get { return _postComments; } }
        public IEnumerable<PostInterAction>? PostInterAction { get { return _postInterAction; } }


        //factory method to create a new post
        public static Post Create(string content, string mediaUrl, PostType postType, PrivacySetting privacySetting, Guid userProfileId)
        {
            return new Post
            {
                Content = content,
                UserProfileId = userProfileId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
        }



        public static void Update(Post post, string content, string mediaUrl, PostType postType, PrivacySetting privacySetting)
        {
            post.Content = content;
            post.UpdatedAt = DateTime.UtcNow;
        }



        public void AddComment(PostComment comment)
        {
            _postComments.Add(comment);
        }

        public void RemoveComment(PostComment comment)
        {
            _postComments.Remove(comment);
        }
        public void AddInteraction(PostInterAction interaction)
        {
            _postInterAction.Add(interaction);
        }
        public void RemoveInteraction(PostInterAction interaction)
        {
            _postInterAction.Remove(interaction);
        }

    }
}
