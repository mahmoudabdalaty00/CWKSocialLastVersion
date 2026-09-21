using Application.Models;
using Application.Service.DTOs.PostDto;

namespace Application.Service.Interface.Services.PostServices;

public interface IPostService
{
    Task<PostResponseDto> GetByIdAsync(int id);
    Task<IReadOnlyList<PostResponseDto>> GetByUserIdAsync(Guid userProfileId);
    Task<IReadOnlyList<PostResponseDto>> GetAllActiveAsync();
    Task<OperationResult<PostResponseDto>> CreateAsync(CreatePostDto dto);
    Task<OperationResult<PostResponseDto>> UpdateAsync(int id, UpdatePostDto dto);
    Task<OperationResult<PostResponseDto>> DeleteAsync(int id);
    Task<OperationResult<PostResponseDto>> RestoreAsync(int id);
}
