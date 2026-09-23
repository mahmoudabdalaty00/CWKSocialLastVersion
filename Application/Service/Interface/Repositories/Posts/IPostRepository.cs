using Domain.Models.Posts;

namespace Application.Service.Interface.Repositories.Posts
{
    public interface IPostRepository  
    {
        Task<IReadOnlyList<Post>> GetByUserIdAsync(string userProfileId);
        Task<IReadOnlyList<Post>> GetAllActiveAsync();
        Task<Post?> GetByIdWithDetailsAsync(string id);
    }
}
