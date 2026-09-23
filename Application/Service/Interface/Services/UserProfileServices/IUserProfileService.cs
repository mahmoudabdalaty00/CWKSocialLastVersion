using Application.DTOs.UserProfileDto;
using Application.Models;

namespace Application.Service.Interface.Services.UserProfileServices;

public interface IUserProfileService
{
    Task<OperationResult<UserProfileResponseDto>> GetByIdAsync(string id);
    Task<OperationResult<UserProfileResponseDto>> GetByIdentityUserIdAsync(string identityUserId);
    Task<OperationResult<IEnumerable<UserProfileResponseDto>>> GetAllAsync();
    Task<OperationResult<UserProfileResponseDto>> CreateAsync(CreateUserProfileDto dto);
    Task<OperationResult<UserProfileResponseDto>> UpdateAsync(string id, UpdateUserProfileDto dto);
    Task<OperationResult<UserProfileResponseDto>> DeleteAsync(string id);
    Task<OperationResult<UserProfileResponseDto>> RestoreAsync(string id);
}