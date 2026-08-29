using Domain.Exceptions;
using Domain.Viladators.UserProfileValidators;

namespace Domain.Models.UserProfiles
{
    public class BasicInfo
    {
        private BasicInfo()
        {
        }
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public DateTime DateOfBirth { get; private set; }
        public string Bio { get; private set; } = string.Empty;
        public string Phone { get; private set; } = string.Empty;
        public string EmailAddress { get; private set; } = string.Empty;
        public string CurrentCity { get; private set; } = string.Empty;



        public static BasicInfo Create(string firstName, string lastName, DateTime dateOfBirth, string bio, string phone, string emailAddress, string currentCity)
        {
            
            var basicInfo = new BasicInfo
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                Bio = bio,
                Phone = phone,
                EmailAddress = emailAddress,
                CurrentCity = currentCity
            };

            var validate = new BasicInfoValidate();

            var validateResult = validate.Validate(basicInfo);

            if (validateResult.IsValid)
                return basicInfo;

            var exception = new UserProfileNotValideException("BasicInfo is not valid");
            foreach (var error in validateResult.Errors)
            {
                exception.ValidationErrors.Add(error.ErrorMessage);
            }
            throw exception;
        }


    }
}
