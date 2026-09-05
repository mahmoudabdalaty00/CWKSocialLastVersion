using Application.Service.DTOs.PostInterActionDto;

namespace Application.Service.Interface.Services.PostServices;

public interface IPostInterActionService
{
    Task<PostInterActionResponseDto> GetByIdAsync(int id);
    Task<IReadOnlyList<PostInterActionResponseDto>> GetByPostIdAsync(int postId);
    Task<IReadOnlyList<PostInterActionResponseDto>> GetAllActiveByPostIdAsync(int postId);
    Task<PostInterActionResponseDto> CreateAsync(CreatePostInterActionDto dto);
    Task<PostInterActionResponseDto> UpdateAsync(int id, UpdatePostInterActionDto dto);
    Task DeleteAsync(int id);
    Task RestoreAsync(int id);
}
