using Domain.Models.Posts;
using FluentValidation;

namespace Domain.Viladators.PostValidators
{
    public class PostInterActionValidator : AbstractValidator<PostInterAction>
    {
        public PostInterActionValidator()
        {
           


            RuleFor(x => x.ReactionType)
                .IsInEnum().WithMessage("Invalid reaction type.");

            RuleFor(x => x.CreatedAt)
                .NotEmpty().WithMessage("Created date is required.")
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Created date cannot be in the future.");

            RuleFor(x => x.IsDeleted)
                .Equal(false).WithMessage("Cannot create a deleted interaction.")
                .When(x => x.Id == 0); // Only for new interactions
        }
    }
}