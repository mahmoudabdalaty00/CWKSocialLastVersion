using Domain.Models.UserProfiles;


namespace Application.Service.Interface.Repositories.UserProfiles
{
    public interface IUserProfileRepository  
    {
        Task<UserProfile?> GetByIdentityUserIdAsync(string identityUserId);
        Task<IReadOnlyList<UserProfile>> GetAllActiveAsync();
    }
}
