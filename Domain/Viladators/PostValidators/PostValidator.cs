using Domain.Models.Posts;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Viladators.PostValidators
{
    public class PostValidator : AbstractValidator<Post>
    {
        private const int MinContentLength = 1;
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


            RuleFor(x => x.CreatedAt)
                .NotEmpty().WithMessage("Created date is required.")
                .Equal(DateTime.UtcNow).WithMessage("Created date cannot be in the future.");
        }

         
    }
}