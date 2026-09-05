using Application.Service.Interface.Common;
using Domain.Models.Posts;

namespace Application.Service.Interface.Repositories.Posts
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<IReadOnlyList<Post>> GetByUserIdAsync(Guid userProfileId);
        Task<IReadOnlyList<Post>> GetAllActiveAsync();
        Task<Post?> GetByIdWithDetailsAsync(int id);
    }
}
