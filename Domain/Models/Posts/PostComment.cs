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

        public int PostId { get; private set; }
        public string Comment { get; private set; }
        public Guid UserProfileId { get; private set; }
        public Post? Post { get; private set; }
        public UserProfile? UserProfile { get; private set; }


        //Factyory method to create a new comment
        public static PostComment Create(int postId, string text, Guid userProfileId)
        {
            if (postId <= 0)
                throw new ArgumentException("PostId Not Valid", nameof(postId));
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Comment cannot be empty", nameof(text));
            if (userProfileId == Guid.Empty)
                throw new ArgumentException("UserProfileId cannot be empty", nameof(userProfileId));


            var post = new PostComment
            {
                PostId = postId,
                Comment = text.Trim(),
                UserProfileId = userProfileId,
            };

            // Validate the post
            var validate = new PostCommentValidator();
            var validationResult = validate.Validate(post);
            if (!validationResult.IsValid)
            {
                var exception = new PostCommentNotValideException("Invalid post comment.");

                exception.ValidationErrors.AddRange(
                    validationResult.Errors.Select(e => e.ErrorMessage));

                throw exception;
            }
            post.InitializeAudit();
            return post;

        }



        public  void Update(string text)
        {

            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentException("Text cannot be empty", nameof(text));

            if (IsDeleted)
                throw new InvalidOperationException("Cannot update a deleted comment");


            Comment = text.Trim();
            SetUpdatedAt();

        }


    }
}
