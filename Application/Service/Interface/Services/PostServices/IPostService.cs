
using Application.DTOs.PostDto;
using Application.Models;

namespace Application.Service.Interface.Services.PostServices;

public interface IPostService
{
    Task<PostResponseDto> GetByIdAsync(string id);
    Task<IReadOnlyList<PostResponseDto>> GetByUserIdAsync(string userProfileId);
    Task<IReadOnlyList<PostResponseDto>> GetAllActiveAsync();
    Task<OperationResult<PostResponseDto>> CreateAsync(CreatePostDto dto);
    Task<OperationResult<PostResponseDto>> UpdateAsync(string id, UpdatePostDto dto);
    Task<OperationResult<PostResponseDto>> DeleteAsync(string id);
    Task<OperationResult<PostResponseDto>> RestoreAsync(string id);
}
