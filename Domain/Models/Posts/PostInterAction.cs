using Domain.Exceptions;
using Domain.Models.BaseEntities;
using Domain.Models.Conasts;
using Domain.Models.UserProfiles;
using Domain.Viladators.PostValidators;
namespace Domain.Models.Posts
{
    public class PostInterAction : BaseEntity<int>
    {
        private PostInterAction()
        {
        }

   
        public ReactionType ReactionType { get; private set; }



        // Navigation properties
        public string PostId { get; private set; }
        public Post Post { get; private set; }

        // Audit trail properties
        public string CreatedById { get; private set; }
        public UserProfile CreatedBy { get; private set; }





        #region Methods

        /// <summary>
        /// Factory method to create a new post interaction/reaction
        /// </summary>
        public static PostInterAction Create(string postId, ReactionType reaction, string createdById)
        {
            if (string.IsNullOrEmpty(postId))
                throw new ArgumentException("PostId not valid", nameof(postId));

            if (!Enum.IsDefined(typeof(ReactionType), reaction))
                throw new ArgumentException("Invalid ReactionType", nameof(reaction));

            var now = DateTime.UtcNow;
            var interaction = new PostInterAction
            {
                PostId = postId,
                ReactionType = reaction,
                CreatedById = createdById,
                CreatedAt = now,
                UpdatedAt = now,
            };

            // Validate the interaction
            var validator = new PostInterActionValidator();
            var validationResult = validator.Validate(interaction);

            if (!validationResult.IsValid)
            {
                var exception = new PostInterActionNotValidException("Invalid post interaction.");
                exception.ValidationErrors.AddRange(
                    validationResult.Errors.Select(e => e.ErrorMessage));

                throw exception;
            }

            return interaction;
        }

        /// <summary>
        /// Update the reaction type
        /// </summary>
        public void Update(ReactionType reaction)
        {
            if (!Enum.IsDefined(typeof(ReactionType), reaction))
                throw new ArgumentException("Invalid ReactionType", nameof(reaction));

            if (IsDeleted)
                throw new InvalidOperationException("Cannot update a deleted interaction");
      

            // Validate after update
            var validator = new PostInterActionValidator();
            var validationResult = validator.Validate(this);

            if (!validationResult.IsValid)
            {
                var exception = new PostInterActionNotValidException("Invalid post interaction after update.");
                exception.ValidationErrors.AddRange(
                    validationResult.Errors.Select(e => e.ErrorMessage));

                throw exception;
            }

            ReactionType = reaction;
            UpdatedAt = DateTime.UtcNow;


            base.SetUpdatedAt();  
        }

        /// <summary>
        /// Delete the interaction
        /// </summary>
        public void Delete()
        {
            if (IsDeleted)
                return; // Already deleted

            base.Delete();
        }

        #endregion
    }
}