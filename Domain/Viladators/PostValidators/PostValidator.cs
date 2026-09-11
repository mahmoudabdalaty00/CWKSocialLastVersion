using Domain.Models.Posts;
using FluentValidation;

namespace Domain.Viladators.PostValidators
{
    public class PostValidator : AbstractValidator<Post>
    {
        private const int MinContentLength = 0;
        private const int MaxContentLength = 10000;

        public PostValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Post content should not be empty.")
                .NotNull().WithMessage("Post content should not be null.")
                .Length(MinContentLength, MaxContentLength)
                .WithMessage($"Post content must be between {MinContentLength} and {MaxContentLength} characters.");

            RuleFor(x => x.UserProfileId)
                .NotEqual(Guid.Empty).WithMessage("User profile ID cannot be empty.");




            RuleFor(x => x.PostType)
            .IsInEnum().WithMessage("Invalid post type specified.");

            RuleFor(x => x.PrivacySetting)
                .IsInEnum().WithMessage("Invalid privacy setting specified.");



            When(x => !string.IsNullOrEmpty(x.MediaUrl), () =>
            {
                RuleFor(x => x.MediaUrl)
                    .Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _))
                    .WithMessage("Media URL must be a valid absolute URL.");
            });

            // Rules specifically for Creation
            RuleSet("Create", () =>
            {
                RuleFor(x => x.CreatedAt)
                    .NotEmpty().WithMessage("Created date is required.")
                    .LessThanOrEqualTo(_ => DateTime.UtcNow.AddMinutes(1))
                    .WithMessage("Created date cannot be in the future.");
            });

        }

         
    }
}