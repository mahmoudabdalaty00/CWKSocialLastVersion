using Application.DTOs.PostInterActionDto;
using Application.Service.Interface.Base;

namespace Application.Service.Interface.Services.PostServices;

public interface IPostInterActionService :IService
{
    Task<PostInterActionResponseDto> GetByIdAsync(int id);
    Task<IReadOnlyList<PostInterActionResponseDto>> GetByPostIdAsync(string postId);
    //Task<IReadOnlyList<PostInterActionResponseDto>> GetAllActiveByPostIdAsync(string postId);
    Task<PostInterActionResponseDto> CreateAsync(CreatePostInterActionDto dto);
    Task<PostInterActionResponseDto> UpdateAsync(int id, UpdatePostInterActionDto dto);
    Task DeleteAsync(int id);
    Task RestoreAsync(int id);
}
