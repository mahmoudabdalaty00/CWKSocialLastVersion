using Domain.Exceptions;
using Domain.Models.BaseEntities;
using Domain.Models.Conasts;
using Domain.Viladators.PostValidators;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using UserProfile = Domain.Models.UserProfiles.UserProfile;
namespace Domain.Models.Posts
{
    public class Post : BaseEntity<string>
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


        // Audit trail properties
        public string CreatedById { get; private set; }
        public UserProfile CreatedBy { get; private set; }

        public string UpdatedById { get; private set; }
        public UserProfile UpdatedBy { get; private set; }

        public string DeletedById { get; private set; }
        public UserProfile DeletedBy { get; private set; }

        public IEnumerable<PostComment> PostComments => _postComments.AsReadOnly();
        public IEnumerable<PostInterAction> PostInterActions => _postInterActions.AsReadOnly();

        #region Methods

        /// <summary>
        /// Factory method to create a new post
        /// </summary>
        public static Post Create(
            string content,
            string? mediaUrl,
            PostType postType,
            PrivacySetting privacySetting,
            string createdById)
        {
            

            if (string.IsNullOrEmpty(createdById))
                throw new ArgumentException("CreatedById cannot be empty", nameof(createdById));

            var now = DateTime.UtcNow;
            var post = new Post
            {
                Content = content?.Trim() ?? string.Empty,
                MediaUrl = mediaUrl?.Trim(),
                PostType = postType,
                PrivacySetting = privacySetting,
                CreatedById = createdById,
                UpdatedById = createdById,
                CreatedAt = now,
                UpdatedAt = now,
            };

            // Validate the post
            var validator = new PostValidator();
            var result = validator.Validate(post);

            if (!result.IsValid)
            {
                var exception = new PostNotValidException("Post validation failed");
                exception.ValidationErrors.AddRange(
                    result.Errors.Select(e => e.ErrorMessage));

                throw exception;
            }

            return post;
        }

        /// <summary>
        /// Update post content and settings
        /// </summary>
        public void Update(
            string content,
            string? mediaUrl,
            PostType postType,
            PrivacySetting privacySetting,
            string updatedById)
        {
            if (IsDeleted)
                throw new InvalidOperationException("Cannot update a deleted post");

            if (string.IsNullOrEmpty(updatedById))
                throw new ArgumentException("UpdatedById cannot be empty", nameof(updatedById));

            Content = content?.Trim() ?? string.Empty;
            MediaUrl = mediaUrl?.Trim();
            PostType = postType;
            PrivacySetting = privacySetting;
            UpdatedById = updatedById;
            UpdatedAt = DateTime.UtcNow;

            var validator = new PostValidator();
            var result = validator.Validate(this);

            if (!result.IsValid)
            {
                var exception = new PostNotValidException("Post validation failed");
                exception.ValidationErrors.AddRange(result.Errors.Select(e => e.ErrorMessage));
                throw exception;
            }
        }

        /// <summary>
        /// Add a comment to the post
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

        /// <summary>
        /// Remove a comment from the post
        /// </summary>
        public void RemoveComment(PostComment comment)
        {
            if (comment == null)
                throw new ArgumentNullException(nameof(comment));
            if (comment.PostId != Id)
                throw new InvalidOperationException("Comment does not belong to this post");
            if (IsDeleted)
                throw new InvalidOperationException("Cannot remove comments from a deleted post");

            _postComments.Remove(comment);
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Add an interaction (like, reaction) to the post
        /// </summary>
        public void AddInteraction(PostInterAction interaction)
        {
            if (interaction == null)
                throw new ArgumentNullException(nameof(interaction));
            if (interaction.PostId != Id)
                throw new InvalidOperationException("Interaction does not belong to this post");
            if (IsDeleted)
                throw new InvalidOperationException("Cannot add interactions to a deleted post");

            _postInterActions.Add(interaction);
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Remove an interaction from the post
        /// </summary>
        public void RemoveInteraction(PostInterAction interaction)
        {
            if (interaction == null)
                throw new ArgumentNullException(nameof(interaction));

            _postInterActions.Remove(interaction);
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Get count of non-deleted interactions
        /// </summary>
        public int GetInteractionCount() => _postInterActions.Count(x => !x.IsDeleted);

        /// <summary>
        /// Get count of non-deleted comments
        /// </summary>
        public int GetCommentCount() => _postComments.Count(x => !x.IsDeleted);

        /// <summary>
        /// Delete the post and cascade delete all comments and interactions
        /// </summary>
        public void Delete(string deletedById)
        {
            if (IsDeleted)
                return; // Already deleted

            if (string.IsNullOrEmpty(deletedById))
                throw new ArgumentException("DeletedById cannot be empty", nameof(deletedById));

            DeletedById = deletedById;

            // Cascade delete comments and interactions
            foreach (var comment in _postComments)
                comment.Delete();

            foreach (var interaction in _postInterActions)
                interaction.Delete();

            base.Delete();
        }

        #endregion
    }
}
