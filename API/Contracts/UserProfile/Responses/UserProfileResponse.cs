using Domain.Models.BaseEntities;

namespace API.Contracts.UserProfile.Responses
{
    public record UserProfileResponse 
    {
        public Guid Id { get; set; } = default!;

        public BasicInformation BasicInfo { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
