using Application.Service.Interface.Common;
using Domain.Models.UserProfiles;


namespace Application.Service.Interface.Repositories.UserProfiles
{
    public interface IUserProfileRepository :IGenericRepository<UserProfile>
    {
        Task<UserProfile?> GetByIdentityUserIdAsync(string identityUserId);
        Task<IReadOnlyList<UserProfile>> GetAllActiveAsync();
    }
}
