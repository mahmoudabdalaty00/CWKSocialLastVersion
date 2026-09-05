using Application.Service.Interface.Common;
using Domain.Models.Posts;

namespace Application.Service.Interface.Repositories.Posts
{
    public interface IPostInterActionRepository : IGenericRepository<PostInterAction>
    {
        Task<IReadOnlyList<PostInterAction>> GetByPostIdAsync(int postId);
        Task<IReadOnlyList<PostInterAction>> GetAllActiveByPostIdAsync(int postId);
    }
}
