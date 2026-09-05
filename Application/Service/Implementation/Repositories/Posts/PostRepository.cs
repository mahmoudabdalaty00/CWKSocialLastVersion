using Application.Service.Implementation.Common;
using Application.Service.Interface.Repositories.Posts;
using Data.MainDb;
using Domain.Models.Posts;
using Microsoft.EntityFrameworkCore;

namespace Application.Service.Implementation.Repositories.Posts;

public class PostRepository : GenericRepository<Post>, IPostRepository
{
    private readonly DataContext _context;

    public PostRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Post>> GetByUserIdAsync(Guid userProfileId)
    {
        return await _context.Posts
            .Where(p => p.UserProfileId == userProfileId && !p.IsDeleted)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Post>> GetAllActiveAsync()
    {
        return await _context.Posts
            .Where(p => !p.IsDeleted)
            .ToListAsync();
    }

    public async Task<Post?> GetByIdWithDetailsAsync(int id)
    {
        return await _context.Posts
            .Include(p => p.PostComments)
            .Include(p => p.PostInterAction)
            .FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted);
    }
}
