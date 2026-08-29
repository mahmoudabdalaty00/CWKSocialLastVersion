using Application.Features.UserProfiles.Commands;
using Data.MainDb;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserProfiles.Validations
{
    public class UpdateUserProfileCommandValidator : AbstractValidator<UpdateUserProfileCommand>
    {
        private readonly DataContext _context;

        public UpdateUserProfileCommandValidator(DataContext context)
        {
            _context = context;

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("User profile ID is required.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MinimumLength(3).WithMessage("First name must be at least 3 characters.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MinimumLength(3).WithMessage("Last name must be at least 3 characters.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(x => x.EmailAddress)
                .NotEmpty().WithMessage("Email address is required.")
                .EmailAddress().WithMessage("A valid email address is required.")
                .MaximumLength(100).WithMessage("Email Address cannot exceed 100 characters.") 
                .MustAsync(BeUniqueEmailForOtherUsers).WithMessage("This email address is already in use by another user.");

            RuleFor(x => x.Bio)
                .MaximumLength(500).WithMessage("Bio cannot exceed 500 characters.");

            RuleFor(x => x.Phone)
                .Matches(@"^\+?[1-9]\d{1,14}$")
                .WithMessage("Phone number must be a valid international format.")
                .When(x => !string.IsNullOrEmpty(x.Phone));

            RuleFor(x => x.CurrentCity)
                .MaximumLength(100).WithMessage("Current city cannot exceed 100 characters.");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().WithMessage("Date of birth is required.")
                .LessThan(DateTime.UtcNow).WithMessage("Date of birth must be in the past.");
        }

        private async Task<bool> BeUniqueEmailForOtherUsers(UpdateUserProfileCommand command, string email, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(email))
                return true;

            // Check if any OTHER user (different ID) has this email address
            bool exists = await _context.UserProfiles
                .AnyAsync(u => u.BasicInfo.EmailAddress.ToLower() == email.ToLower()
                            && u.Id != command.Id, cancellationToken);

            return !exists;
        }
    }
}
