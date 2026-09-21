using Domain.Exceptions;
using Domain.Models.BaseEntities;
using Domain.Models.UserProfiles;
using Domain.Viladators.PostValidators;
using static System.Net.Mime.MediaTypeNames;
namespace Domain.Models.Posts
{
    public class PostComment : BaseEntity<int>
    {
        private PostComment()
        {
        }

        public string PostId { get; private set; }
        public string Comment { get; private set; } = string.Empty;
        public Post? Post { get; private set; }


        // Audit trail properties
        public string CreatedById { get; private set; }
        public UserProfile CreatedBy { get; private set; }

        public string UpdatedById { get; private set; }
        public UserProfile UpdatedBy { get; private set; }

        public string DeletedById { get; private set; }
        public UserProfile DeletedBy { get; private set; }

        #region Methods

        /// <summary>
        /// Factory method to create a new post comment
        /// </summary>
        public static PostComment Create(string postId, string text,string createdById)
        {
            if (string.IsNullOrEmpty(postId))
                throw new ArgumentException("PostId not valid", nameof(postId));

            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Comment cannot be empty", nameof(text));

            if (string.IsNullOrEmpty(createdById))
                throw new ArgumentException("CreatedById cannot be empty", nameof(createdById));

            var now = DateTime.UtcNow;
            var comment = new PostComment
            {
                PostId = postId,
                Comment = text.Trim(),
                CreatedById = createdById,
                UpdatedById = createdById,
                CreatedAt = now,
                UpdatedAt = now,
            };

            // Validate the comment
            var validator = new PostCommentValidator();
            var validationResult = validator.Validate(comment);

            if (!validationResult.IsValid)
            {
                var exception = new PostCommentNotValideException("Invalid post comment.");
                exception.ValidationErrors.AddRange(
                    validationResult.Errors.Select(e => e.ErrorMessage));

                throw exception;
            }

            return comment;
        }

        /// <summary>
        /// Update the comment text
        /// </summary>
        public void Update(string text, string updatedById)
        {
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Text cannot be empty", nameof(text));

            if (string.IsNullOrEmpty(updatedById))
                throw new ArgumentException("UpdatedById cannot be empty", nameof(updatedById));

            if (IsDeleted)
                throw new InvalidOperationException("Cannot update a deleted comment");

            Comment = text.Trim();
            UpdatedById = updatedById;
            UpdatedAt = DateTime.UtcNow;

            // Validate after update
            var validator = new PostCommentValidator();
            var validationResult = validator.Validate(this);

            if (!validationResult.IsValid)
            {
                var exception = new PostCommentNotValideException("Invalid post comment after update.");
                exception.ValidationErrors.AddRange(
                    validationResult.Errors.Select(e => e.ErrorMessage));

                throw exception;
            }
        }

        /// <summary>
        /// Delete the comment
        /// </summary>
        public void Delete(string deletedById)
        {
            if (IsDeleted)
                return; // Already deleted

            if (string.IsNullOrEmpty(deletedById))
                throw new ArgumentException("DeletedById cannot be empty", nameof(deletedById));

            DeletedById = deletedById;
            base.Delete();
        }

        #endregion
    }
}
