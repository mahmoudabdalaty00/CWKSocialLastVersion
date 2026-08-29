using Domain.Models.UserProfiles;
using FluentValidation;

namespace Domain.Viladators.UserProfileValidators
{
    public class UserProfileValidator : AbstractValidator<UserProfile>
        {
            public UserProfileValidator()
            {
                RuleFor(x => x.IdentityUserId)
                    .NotEmpty().WithMessage("Identity user ID is required.")
                    .Length(1, 128).WithMessage("Identity user ID must be between 1 and 128 characters.");

                RuleFor(x => x.BasicInfo)
                    .NotNull().WithMessage("Basic information is required.")
                    .SetValidator(new BasicInfoValidate());
            }
        }
    }


 
