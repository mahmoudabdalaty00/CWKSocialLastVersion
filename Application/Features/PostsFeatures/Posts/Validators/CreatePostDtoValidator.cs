using Application.Service.DTOs.PostDto;
using Application.Service.DTOs.UserProfileDto;
using Data.MainDb;
using Domain.Models.Conasts;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Application.Features.PostsFeatures.Posts.Validators
{
    public class CreatePostDtoValidator : AbstractValidator<CreatePostDto>
    {
        private readonly DataContext _context;

        public CreatePostDtoValidator(DataContext context)
        {
            _context = context;
            RuleFor(x => x.Content)
                      .NotEmpty().WithMessage("Content is required.")
                      .MinimumLength(3).WithMessage("Content must be at least 3 characters.")
                      .MaximumLength(500).WithMessage("Content must be at most 500 characters.");

            RuleFor(x => x.UserProfileId)
                .NotEmpty().WithMessage("UserProfileId is required.")
                .MustAsync(BeExistingUserProfile).WithMessage("The specified UserProfileId does not exist.");

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

        
 


        private async Task<bool> BeExistingUserProfile(Guid userProfileId, CancellationToken cancellationToken)
        {
            // Check if the UserProfileId exists in the database
            bool exists = await _context.UserProfiles
                .AnyAsync(u => u.Id == userProfileId, cancellationToken);
            return exists;
        }
    }
}

 
