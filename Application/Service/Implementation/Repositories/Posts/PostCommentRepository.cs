using Application.Service.Implementation.Common;
using Application.Service.Interface.Repositories.Posts;
using Data.MainDb;
using Domain.Models.Posts;
using Microsoft.EntityFrameworkCore;

namespace Application.Service.Implementation.Repositories.Posts;

public class PostCommentRepository : GenericRepository<PostComment>, IPostCommentRepository
{
    private readonly DataContext _context;

    public PostCommentRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<PostComment>> GetByPostIdAsync(int postId)
    {
        return await _context.PostComments
            .Where(c => c.PostId == postId)
            .ToListAsync();
    }

    public async Task<IReadOnlyList<PostComment>> GetAllActiveByPostIdAsync(int postId)
    {
        return await _context.PostComments
            .Where(c => c.PostId == postId && !c.IsDeleted)
            .ToListAsync();
    }
}
