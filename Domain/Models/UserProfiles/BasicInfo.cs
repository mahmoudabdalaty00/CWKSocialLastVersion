using Domain.Exceptions;
using Domain.Viladators.UserProfileValidators;

namespace Domain.Models.UserProfiles
{
    public sealed class BasicInfo
    {
        private BasicInfo()
        {
        }

        private BasicInfo(
       string firstName,
       string lastName,
       DateOnly dateOfBirth,
       string bio,
       string phone,
       string emailAddress,
       string currentCity)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Bio = bio;
            Phone = phone;
            EmailAddress = emailAddress;
            CurrentCity = currentCity;
        }

        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public DateOnly DateOfBirth { get; private set; }
        public string Bio { get; private set; } = string.Empty;
        public string Phone { get; private set; } = string.Empty;
        public string EmailAddress { get; private set; } = string.Empty;
        public string CurrentCity { get; private set; } = string.Empty;




        public static BasicInfo Create(
            string firstName, string lastName, DateOnly dateOfBirth,
            string bio, string phone, string emailAddress, string currentCity)
        {

            var basicInfo = new BasicInfo
            {
                FirstName = firstName.Trim() ?? string.Empty,
                LastName = lastName.Trim() ?? string.Empty,
                DateOfBirth = dateOfBirth,
                Bio = bio.Trim() ?? string.Empty,
                Phone = phone.Trim() ?? string.Empty,
                EmailAddress = emailAddress.Trim() ?? string.Empty,
                CurrentCity = currentCity.Trim() ?? string.Empty
            };

            var validate = new BasicInfoValidator();

            var validateResult = validate.Validate(basicInfo);

            if (validateResult.IsValid)
                return basicInfo;

            var exception = new UserProfileNotValideException("BasicInfo is not valid");
            exception.ValidationErrors
                .AddRange(validateResult.Errors
                .Select(error => error.ErrorMessage));

            throw exception;
        }


    }
}
