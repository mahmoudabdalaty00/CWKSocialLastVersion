using Domain.Models.BaseEntities;
using Domain.Models.Conasts;
using Domain.Models.UserProfiles;
namespace Domain.Models.Posts
{
    public class PostInterAction : BaseEntity<int>
    {
        private PostInterAction()
        {

        }

        public int PostId { get; private set; }
        public Guid UserProfileId { get; private set; }
        public ReactionType ReactionType { get; private set; }

        // Navigation properties
        public Post? Post { get; private set; }
        public UserProfile? UserProfile { get; private set; }


        /// <summary>
        /// Factory method to create a new reaction
        /// </summary>
        public static PostInterAction Create(int postId, Guid userProfileId,ReactionType reaction)
        {

            if (postId <= 0) 
                throw new ArgumentException("PostId not Valid", nameof(postId));
            if (userProfileId == Guid.Empty) 
                throw new ArgumentException("UserProfileId cannot be empty", nameof(userProfileId));
            if (!Enum.IsDefined(typeof(ReactionType), reaction))
                throw new ArgumentException("Invalid ReactionType", nameof(reaction));


            var interaction = new PostInterAction
            {
                PostId = postId,
                UserProfileId = userProfileId,
                ReactionType = reaction,
            };

            interaction.InitializeAudit();
            return interaction;

        }


        public void Update(ReactionType reaction)
        {
            if (!Enum.IsDefined(typeof(ReactionType), reaction))
                throw new ArgumentException("Invalid ReactionType", nameof(reaction));

            ReactionType = reaction;
            UpdatedAt = DateTime.UtcNow;
        }




    }
}
