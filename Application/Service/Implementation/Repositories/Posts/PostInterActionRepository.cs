using Application.Service.Implementation.Common;
using Application.Service.Interface.Repositories.Posts;
using Data.MainDb;
using Domain.Models.Posts;
using Microsoft.EntityFrameworkCore;

namespace Application.Service.Implementation.Repositories.Posts;

public class PostInterActionRepository : GenericRepository<PostInterAction>, IPostInterActionRepository
{
    private readonly DataContext _context;

    public PostInterActionRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<PostInterAction>> GetByPostIdAsync(int postId)
    {
        return await _context.PostInterActions
            .Where(i => i.PostId == postId)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<PostInterAction>> GetAllActiveByPostIdAsync(int postId)
    {
        return await _context.PostInterActions
            .Where(i => i.PostId == postId && !i.IsDeleted)
            .ToListAsync();
    }
}
