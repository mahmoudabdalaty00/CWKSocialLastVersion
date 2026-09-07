using Application.Models;
using Application.Service.DTOs.UserProfileDto;

namespace Application.Service.Interface.Services.UserProfileServices;

public interface IUserProfileService
{
    Task<OperationResult<UserProfileResponseDto>> GetByIdAsync(Guid id);
    Task<OperationResult<UserProfileResponseDto>> GetByIdentityUserIdAsync(string identityUserId);
    Task<OperationResult<IEnumerable<UserProfileResponseDto>>> GetAllAsync();
    Task<OperationResult<UserProfileResponseDto>> CreateAsync(CreateUserProfileDto dto);
    Task<OperationResult<UserProfileResponseDto>> UpdateAsync(Guid id, UpdateUserProfileDto dto);
    Task<OperationResult<UserProfileResponseDto>> DeleteAsync(Guid id);
    Task<OperationResult<UserProfileResponseDto>> RestoreAsync(Guid id);
}