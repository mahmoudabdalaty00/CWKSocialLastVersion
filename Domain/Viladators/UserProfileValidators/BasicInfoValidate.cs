using Domain.Models.UserProfiles;
using FluentValidation;

namespace Domain.Viladators.UserProfileValidators
{
    public class BasicInfoValidate : AbstractValidator<BasicInfo>
    {
        public BasicInfoValidate()
        {
            RuleFor(x => x.FirstName)
              .NotEmpty().WithMessage("First name is required.")
              .Length(3, 50).WithMessage("First name must be between 3 and 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .Length(3, 50).WithMessage("Last name must be between 3 and 50 characters.");

            RuleFor(x => x.DateOfBirth)
                  .NotEmpty().WithMessage("Date of birth is required.")
                  .LessThan(DateTime.Today.AddYears(-10)).WithMessage("Person must be at least 10 years old.")
                  .GreaterThan(DateTime.Today.AddYears(-120)).WithMessage("Please enter a valid date of birth.");
            RuleFor(x => x.Bio).MaximumLength(500).WithMessage("Bio must not exceed 500 characters.");

            RuleFor(x => x.Phone)
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone number must be in a valid format (E.164 format).")
                .When(x => !string.IsNullOrEmpty(x.Phone));

            RuleFor(x => x.EmailAddress)
                .EmailAddress().WithMessage("A valid email address is required.")
                .When(x => !string.IsNullOrEmpty(x.EmailAddress));

            RuleFor(x => x.CurrentCity)
                .Length(2, 100).WithMessage("City name must be between 2 and 100 characters.")
                .When(x => !string.IsNullOrEmpty(x.CurrentCity));
        }
    }
}



