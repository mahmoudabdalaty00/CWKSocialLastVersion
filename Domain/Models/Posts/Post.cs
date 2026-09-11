using Domain.Exceptions;
using Domain.Models.BaseEntities;
using Domain.Models.Conasts;
using Domain.Viladators.PostValidators;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using UserProfile = Domain.Models.UserProfiles.UserProfile;
namespace Domain.Models.Posts
{
    public class Post : BaseEntity<int>
    {
        private readonly List<PostComment> _postComments = new List<PostComment>();
        private readonly List<PostInterAction> _postInterActions = new List<PostInterAction>();
        private Post()
        {

        }

        public string Content { get; private set; } = string.Empty;
        public PostType PostType { get; private set; }
        public string? MediaUrl { get; private set; }
        public PrivacySetting PrivacySetting { get; private set; }
        [ForeignKey(nameof(UserProfile))]
        public Guid UserProfileId { get; private set; }
        public UserProfile UserProfile { get; private set; }
        public IEnumerable<PostComment> PostComments => _postComments.AsReadOnly();
        public IEnumerable<PostInterAction> PostInterActions => _postInterActions.AsReadOnly();




        //factory method to create a new post
        public static Post Create(
               string content, string? mediaUrl,
                 PostType postType,
                 PrivacySetting privacySetting,
                 Guid userProfileId)
        {


            if (userProfileId == Guid.Empty)
                throw new ArgumentException("UserProfileId cannot be empty", nameof(userProfileId));



            var now = DateTime.UtcNow;
            var post = new Post
            {
                Content = content.Trim() ?? string.Empty,
                MediaUrl = mediaUrl?.Trim(),
                PostType = postType,
                PrivacySetting = privacySetting,
                UserProfileId = userProfileId,
                CreatedAt = now,
                UpdatedAt = now,
            };


            // Validate the post
            var validate = new PostValidator();

            var result = validate.Validate(post);
            if (!result.IsValid)
            {
                var exception = new PostNotValidException("Post validation failed");
                exception.ValidationErrors.AddRange(
                    result.Errors.Select(e => e.ErrorMessage));

                throw exception;
            }
            post.InitializeAudit();
            return post;

        }



        public void Update(string content, string? mediaUrl, PostType postType, PrivacySetting privacySetting)
        {
            if (IsDeleted)
                throw new InvalidOperationException("Cannot update a deleted post");

            Content = content;
            MediaUrl = mediaUrl;
            PostType = postType;
            PrivacySetting = privacySetting;
            UpdatedAt = DateTime.UtcNow;

            var validator = new PostValidator();
            var result = validator.Validate(this);

            if (!result.IsValid)
            {
                var exception = new PostNotValidException("Post validation failed");
                exception.ValidationErrors.AddRange(result.Errors.Select(e => e.ErrorMessage));
                throw exception;
            }
            SetUpdatedAt();
        }


        /// </summary>
        public void AddComment(PostComment comment)
        {
            if (comment == null) 
                throw new ArgumentNullException(nameof(comment));
            if (comment.PostId != Id) 
                throw new InvalidOperationException("Comment does not belong to this post");
            if (IsDeleted)
                throw new InvalidOperationException("Cannot add comments to a deleted post");

            _postComments.Add(comment);
            UpdatedAt = DateTime.UtcNow;
        }


        public void RemoveComment(PostComment comment)
        {
            if (comment == null) 
                throw new ArgumentNullException(nameof(comment));
            if (comment.PostId != Id)
                throw new InvalidOperationException("Comment does not belong to this post");
            if (IsDeleted)
                throw new InvalidOperationException("Cannot add comments to a deleted post");

            _postComments.Remove(comment);
            SetUpdatedAt();
        }
        public void AddInteraction(PostInterAction interaction)
        {
            if (interaction == null) throw new ArgumentNullException(nameof(interaction));
            if (interaction.PostId != Id) throw new InvalidOperationException("Interaction does not belong to this post");
            if (IsDeleted) throw new InvalidOperationException("Cannot add interactions to a deleted post");

            _postInterActions.Add(interaction);
            SetUpdatedAt();
        }


        public void RemoveInteraction(PostInterAction interaction)
        {
            if (interaction == null) throw new ArgumentNullException(nameof(interaction));

            _postInterActions.Remove(interaction);
            SetUpdatedAt();
        }


        public int GetInteractionCount() => _postInterActions.Count(x => !x.IsDeleted);
        public int GetCommentCount() => _postComments.Count(x => !x.IsDeleted);


        public override void Delete()
        {
            if (IsDeleted)
                return; // Already deleted

            // Cascade delete comments and interactions
            foreach (var comment in _postComments)
                comment.Delete();

            foreach (var interaction in _postInterActions)
                interaction.Delete();

            base.Delete();
        }


    }
}
