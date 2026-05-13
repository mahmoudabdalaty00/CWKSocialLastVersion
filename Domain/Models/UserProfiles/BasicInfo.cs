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
            //TO Do:Add Validation,error handling ,error notification
            return new BasicInfo
            {
                FirstName = firstName,
                LastName = lastName,
                DateOfBirth = dateOfBirth,
                Bio = bio,
                Phone = phone,
                EmailAddress = emailAddress,
                CurrentCity = currentCity
            };
        }
         

    }
}
