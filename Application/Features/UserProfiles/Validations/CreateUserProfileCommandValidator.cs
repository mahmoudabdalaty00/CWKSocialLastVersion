using Application.Features.UserProfiles.Commands;
using Data.MainDb;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.UserProfiles.Validations
{
    public class CreateUserProfileCommandValidator : AbstractValidator<CreateUserProfileCommand>
    {
        private readonly DataContext _context;

        public CreateUserProfileCommandValidator(DataContext context)
        {
            _context = context;
            RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MinimumLength(3).WithMessage("First name must be at least 3 characters.")
            .MaximumLength(50).WithMessage("First name must be at most 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MinimumLength(3).WithMessage("Last name must be at least 3 characters.")
                .MaximumLength(50).WithMessage("Last name must be at most 50 characters.");

            // Synchronous email format validation followed by Async database check
            RuleFor(x => x.EmailAddress)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("A valid email address is required.")
                .MustAsync(BeUniqueEmail).WithMessage("This email address is already registered.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required.")
                .LessThan(DateTime.UtcNow).WithMessage("Date of birth must be in the past.");
        }

        private async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(email))
                return true;

            // Query the database to check if the email already exists
            bool exists = await _context.UserProfiles
                .AnyAsync(u => u.BasicInfo.EmailAddress.ToLower() == email.ToLower(), cancellationToken);

            return !exists;
        }

    }
}
