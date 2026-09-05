using Application.Service.Implementation.Common;
using Application.Service.Interface.Repositories.UserProfiles;
using Data.MainDb;
using Domain.Models.UserProfiles;
using Microsoft.EntityFrameworkCore;

namespace Application.Service.Implementation.Repositories.UserProfiles;

public class UserProfileRepository : GenericRepository<UserProfile>, IUserProfileRepository
{
    private readonly DataContext _context;

    public UserProfileRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public async Task<UserProfile?> GetByIdentityUserIdAsync(string identityUserId)
    {
        return await _context.UserProfiles
            .FirstOrDefaultAsync(up => up.IdentityUserId == identityUserId && !up.IsDeleted);
    }

    public async Task<IReadOnlyList<UserProfile>> GetAllActiveAsync()
    {
        return await _context.UserProfiles
            .Where(up => !up.IsDeleted)
            .ToListAsync();
    }
}

 