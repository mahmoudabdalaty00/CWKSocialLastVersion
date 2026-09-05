using Application.Service.DTOs.UserProfileDto;

namespace Application.Service.Interface.Services.UserProfileServices;

public interface IUserProfileService
{
    Task<UserProfileResponseDto> GetByIdAsync(Guid id);
    Task<UserProfileResponseDto> GetByIdentityUserIdAsync(string identityUserId);
    Task<IEnumerable<UserProfileResponseDto>> GetAllAsync();
    Task<UserProfileResponseDto> CreateAsync(CreateUserProfileDto dto);
    Task<UserProfileResponseDto> UpdateAsync(Guid id, UpdateUserProfileDto dto);
    Task DeleteAsync(Guid id);
    Task RestoreAsync(Guid id);
}