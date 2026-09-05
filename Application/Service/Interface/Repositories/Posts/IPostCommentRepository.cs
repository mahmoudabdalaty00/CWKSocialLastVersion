using Application.Service.Interface.Common;
using Domain.Models.Posts;

namespace Application.Service.Interface.Repositories.Posts
{
    public interface IPostCommentRepository : IGenericRepository<PostComment>
    {
        Task<IReadOnlyList<PostComment>> GetByPostIdAsync(int postId);
        Task<IReadOnlyList<PostComment>> GetAllActiveByPostIdAsync(int postId);
    }
}
