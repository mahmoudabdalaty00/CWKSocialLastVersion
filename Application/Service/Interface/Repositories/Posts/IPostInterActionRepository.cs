using Domain.Models.Posts;

namespace Application.Service.Interface.Repositories.Posts
{
    public interface IPostInterActionRepository  
    {
        Task<IReadOnlyList<PostInterAction>> GetByPostIdAsync(string postId);
        Task<IReadOnlyList<PostInterAction>> GetAllActiveByPostIdAsync(string postId);
    }
}
