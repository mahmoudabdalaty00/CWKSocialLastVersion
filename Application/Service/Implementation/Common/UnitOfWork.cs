using Application.Service.Implementation.Repositories.Posts;
using Application.Service.Implementation.Repositories.UserProfiles;
using Application.Service.Interface.Common;
using Application.Service.Interface.Repositories.Posts;
using Application.Service.Interface.Repositories.UserProfiles;
using Data.MainDb;

namespace Application.Service.Implementation.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly Dictionary<Type, object> _repositories = new();
        private IUserProfileRepository? _userProfileRepository;
        private IPostRepository? _postRepository;
        private IPostCommentRepository? _postCommentRepository;
        private IPostInterActionRepository? _postInterActionRepository;

        public UnitOfWork(DataContext context) => _context = context;

        public IGenericRepository<T> Repository<T>() where T : class
        {
            if (!_repositories.TryGetValue(typeof(T), out var repo))
            {
                repo = new GenericRepository<T>(_context);
                _repositories[typeof(T)] = repo;
            }

            return (IGenericRepository<T>)repo;
        }

        public IUserProfileRepository UserProfileRepository =>
            _userProfileRepository ??= new UserProfileRepository(_context);

        public IPostRepository PostRepository =>
            _postRepository ??= new PostRepository(_context);

        public IPostCommentRepository PostCommentRepository =>
            _postCommentRepository ??= new PostCommentRepository(_context);

        public IPostInterActionRepository PostInterActionRepository =>
            _postInterActionRepository ??= new PostInterActionRepository(_context);

        public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();
    }
}
