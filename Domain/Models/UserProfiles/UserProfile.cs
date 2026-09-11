using Domain.Models.BaseEntities;
using Domain.Models.Posts;

namespace Domain.Models.UserProfiles
{
    public sealed class UserProfile : BaseEntity<Guid>
    {
        private readonly List<Post> _posts = new List<Post>();
        private readonly List<PostComment> _postComments = new List<PostComment>();
        private readonly List<PostInterAction> _postInterActions = new List<PostInterAction>();

        private UserProfile()
        {

        }
        public string IdentityUserId { get; private set; }
        public BasicInfo BasicInfo { get; private set; }


        // Navigation properties
        public IEnumerable<Post> Posts => _posts.AsReadOnly();
        public IEnumerable<PostComment> PostComments => _postComments.AsReadOnly();
        public IEnumerable<PostInterAction> PostInterActions => _postInterActions.AsReadOnly();




        // Factory method ensures that new UserProfile objects
        // are created with required values and timestamps.
        public static UserProfile Create(
            string identityUserId, 
            BasicInfo basicInfo)
        {


            if (string.IsNullOrWhiteSpace(identityUserId))
            {
                throw new ArgumentException(
                    "Identity user ID is required.",
                    nameof(identityUserId));
            }

            ArgumentNullException.ThrowIfNull(basicInfo);


            var now = DateTime.UtcNow;

            var userProfile = new UserProfile
            {
                
                IdentityUserId = identityUserId,
                BasicInfo = basicInfo,
            };

            userProfile.InitializeAudit();
            return userProfile;
        }


        //public method to update the BasicInfo of the UserProfile
        public void UpdateBasicInfo(BasicInfo basicInfo)
        {
            if (basicInfo == null)
                throw new ArgumentNullException(nameof(basicInfo));

            if (IsDeleted)
                throw new InvalidOperationException("Cannot update a deleted user profile");
           
            BasicInfo = basicInfo;
            SetUpdatedAt();
        }


        internal void AddPost(Post post)
        {
            if (post == null) throw new ArgumentNullException(nameof(post));
            _posts.Add(post);
            SetUpdatedAt();
        }

        internal void RemovePost(Post post)
        {
            _posts.Remove(post);
            SetUpdatedAt();
        }

        internal void AddComment(PostComment comment)
        {
            if (comment == null) throw new ArgumentNullException(nameof(comment));
            _postComments.Add(comment);
            SetUpdatedAt();
        }

        internal void RemoveComment(PostComment comment)
        {
            _postComments.Remove(comment);
            SetUpdatedAt();
        }

        internal void AddInteraction(PostInterAction interaction)
        {
            if (interaction == null) throw new ArgumentNullException(nameof(interaction));
            _postInterActions.Add(interaction);
            SetUpdatedAt();
        }

        internal void RemoveInteraction(PostInterAction interaction)
        {
            _postInterActions.Remove(interaction);
            SetUpdatedAt();
        }

        public override void Delete()
        {
            if (_posts.Count > 0)
                throw new InvalidOperationException("Cannot delete a user profile with active posts");
            IsDeleted = true;
            
            base.Delete();
        }


    }
}
