using System.ComponentModel.DataAnnotations;

namespace Admin.ViewModels;

public sealed class UserProfileListItemViewModel
{
    public Guid Id { get; init; }
    public string FullName { get; init; } = string.Empty;
    public string EmailAddress { get; init; } = string.Empty;
    public string CurrentCity { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
}

public sealed class UserProfileDetailsViewModel
{
    public Guid Id { get; init; }
    public string IdentityUserId { get; init; } = string.Empty;
    public string FullName { get; init; } = string.Empty;
    public DateTime DateOfBirth { get; init; }
    public string Bio { get; init; } = string.Empty;
    public string Phone { get; init; } = string.Empty;
    public string EmailAddress { get; init; } = string.Empty;
    public string CurrentCity { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
}

public sealed class UserProfileFormViewModel
{
    [Display(Name = "Identity user ID")]
    public string IdentityUserId { get; set; } = string.Empty;
    [Required, StringLength(50, MinimumLength = 3)] public string FirstName { get; set; } = string.Empty;
    [Required, StringLength(50, MinimumLength = 3)] public string LastName { get; set; } = string.Empty;
    [Required, DataType(DataType.Date), Display(Name = "Date of birth")] public DateTime? DateOfBirth { get; set; }
    [StringLength(500)] public string Bio { get; set; } = string.Empty;
    [Phone] public string Phone { get; set; } = string.Empty;
    [Required, EmailAddress, StringLength(100), Display(Name = "Email address")] public string EmailAddress { get; set; } = string.Empty;
    [StringLength(100), Display(Name = "Current city")] public string CurrentCity { get; set; } = string.Empty;
}
