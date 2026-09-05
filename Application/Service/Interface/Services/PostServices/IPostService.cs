using Application.Service.DTOs.PostDto;

namespace Application.Service.Interface.Services.PostServices;

public interface IPostService
{
    Task<PostResponseDto> GetByIdAsync(int id);
    Task<IReadOnlyList<PostResponseDto>> GetByUserIdAsync(Guid userProfileId);
    Task<IReadOnlyList<PostResponseDto>> GetAllActiveAsync();
    Task<PostResponseDto> CreateAsync(CreatePostDto dto);
    Task<PostResponseDto> UpdateAsync(int id, UpdatePostDto dto);
    Task DeleteAsync(int id);
    Task RestoreAsync(int id);
}
