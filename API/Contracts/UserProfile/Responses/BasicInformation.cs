namespace API.Contracts.UserProfile.Responses
{
    public record BasicInformation
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Bio { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string CurrentCity { get; set; } = string.Empty;
    }
}
