using Application.DTOs.PostDto;
using Data.MainDb;
using Domain.Models.Conasts;
using FluentValidation;

namespace Application.Features.PostsFeatures.Posts.Validators
{
    public class UpdatePostDtoValidator : AbstractValidator<UpdatePostDto>
    {
        private readonly DataContext _context;

        public UpdatePostDtoValidator(DataContext context)
        {
            _context = context;
            RuleFor(x => x.Content)
                      .NotEmpty().WithMessage("Content is required.")
                      .MinimumLength(3).WithMessage("Content must be at least 3 characters.")
                      .MaximumLength(500).WithMessage("Content must be at most 500 characters.");

         
            RuleFor(x => x.PostType)
                .NotEmpty().WithMessage("PostType is required.")
                .IsInEnum().WithMessage("PostType must be a valid PostType value.");

            // MediaUrl is required only for non-Text post types
            RuleFor(x => x.MediaUrl)
                .NotEmpty().WithMessage("MediaUrl is required for this post type.")
                .When(x => x.PostType != PostType.Text);

            RuleFor(x => x.PrivacySetting)
                .IsInEnum().WithMessage("PrivacySetting must be a valid PrivacySetting value.");
        }





         
    }
}

 
