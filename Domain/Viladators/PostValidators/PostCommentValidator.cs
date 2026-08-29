using Domain.Models.Posts;
using FluentValidation;

namespace Domain.Viladators.PostValidators
{
    public class PostCommentValidator : AbstractValidator<PostComment>
    {
        private const int MinTextLength = 1;
        private const int MaxTextLength = 1000;

        public PostCommentValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty().WithMessage("Comment text should not be empty.")
                 .NotNull().WithMessage("Comment text should not be null.")
                .Length(MinTextLength, MaxTextLength)
                .WithMessage($"Comment text must be between {MinTextLength} and {MaxTextLength} characters.");

            RuleFor(x => x.PostId)
                .GreaterThan(0).WithMessage("Post ID must be greater than 0.");

            RuleFor(x => x.UserProfileId)
                .NotEqual(Guid.Empty).WithMessage("User profile ID cannot be empty.");

            RuleFor(x => x.CreatedAt)
                .NotEmpty().WithMessage("Created date is required.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Created date cannot be in the future.");

            RuleFor(x => x.IsDeleted)
                .Equal(false).WithMessage("Cannot create a deleted comment.")
                .When(x => x.Id == 0); // Only for new comments
        }
    }
}