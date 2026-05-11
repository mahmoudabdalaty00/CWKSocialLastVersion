using Domain.Models.BaseEntities;
using Domain.Models.Conasts;
using UserProfile = Domain.Models.UserProfiles.UserProfile;
namespace Domain.Models.Posts
{
    public class Post : BaseEntity<int>
    {
        public string Content { get; set; } = string.Empty;
        public string MediaUrl { get; set; } = string.Empty;
        public PostType PostType { get; set; } = PostType.Text;
        public PrivacySetting PrivacySetting { get; set; } = PrivacySetting.Public;
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        public Guid UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
        public ICollection<PostComment>? PostComments { get; set; }
        public ICollection<PostInterAction>? PostInterActions { get; set; }
    }

    public class PostComment : BaseEntity<int>
    {
        public int PostId { get; set; }
        public string Text { get; set; }
        public Guid UserProfileId { get; set; }

    }

    public class PostInterAction : BaseEntity<int>  
    {
        public int PostId { get; set; }
        public ReactionType ReactionType { get; set; }
    }
}
