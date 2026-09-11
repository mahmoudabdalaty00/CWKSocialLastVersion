namespace Application.Service.DTOs.UserProfileDto;

public class UserProfileResponseDto
{
    public Guid Id { get; set; }
    public string IdentityUserId { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string Bio { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string CurrentCity { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
}