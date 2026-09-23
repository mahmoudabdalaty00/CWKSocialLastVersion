using Domain.Models.Posts;

namespace Application.Service.Interface.Repositories.Posts
{
    public interface IPostCommentRepository  
    {
        Task<IReadOnlyList<PostComment>> GetByPostIdAsync(string postId);
        Task<IReadOnlyList<PostComment>> GetAllActiveByPostIdAsync(string postId);
    }
}
