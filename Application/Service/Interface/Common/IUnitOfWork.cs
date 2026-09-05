using Application.Service.Interface.Repositories.Posts;
using Application.Service.Interface.Repositories.UserProfiles;

namespace Application.Service.Interface.Common;

    /// <summary>
    /// Hands out repositories that all share the same DbContext, so a single
    /// SaveChangesAsync() commits everything they touched as one transaction.
    /// </summary>
    public interface IUnitOfWork
    {
        IGenericRepository<T> Repository<T>() where T : class;
        Task<int> SaveChangesAsync();
        
        IUserProfileRepository UserProfileRepository { get; }
        IPostRepository PostRepository { get; }
        IPostCommentRepository PostCommentRepository { get; }
        IPostInterActionRepository PostInterActionRepository { get; }
    }


