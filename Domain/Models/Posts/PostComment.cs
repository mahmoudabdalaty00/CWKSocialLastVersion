using Domain.Exceptions;
using Domain.Models.BaseEntities;
using Domain.Viladators.PostValidators;
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

            var validate = new PostCommentValidator();
          
            var post = new PostComment
            {
                PostId = postId,
                Text = text.Trim(),
                UserProfileId = userProfileId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            // Validate the post
            var validationResult = validate.Validate(post);
            if (validationResult.IsValid)
              return post;

            var exception = new PostCommentNotValideException("Invalid post comment.");
             
            exception.ValidationErrors.AddRange(
                validationResult.Errors.Select(e => e.ErrorMessage));

            throw exception;
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
