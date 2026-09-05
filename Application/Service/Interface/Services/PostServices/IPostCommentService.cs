using Application.Service.DTOs.PostCommentDto;

namespace Application.Service.Interface.Services.PostServices;

public interface IPostCommentService
{
    Task<PostCommentResponseDto> GetByIdAsync(int id);
    Task<IReadOnlyList<PostCommentResponseDto>> GetByPostIdAsync(int postId);
    Task<IReadOnlyList<PostCommentResponseDto>> GetAllActiveByPostIdAsync(int postId);
    Task<PostCommentResponseDto> CreateAsync(CreatePostCommentDto dto);
    Task<PostCommentResponseDto> UpdateAsync(int id, UpdatePostCommentDto dto);
    Task DeleteAsync(int id);
    Task RestoreAsync(int id);
}
