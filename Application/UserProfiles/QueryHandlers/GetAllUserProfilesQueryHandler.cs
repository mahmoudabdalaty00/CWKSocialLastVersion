using Application.UserProfiles.Queries;
using Data.MainDb;
using Domain.Models.UserProfiles;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UserProfiles.QueryHandlers
{
    public class GetAllUserProfilesQueryHandler : IRequestHandler<GetAllUserProfilesQuery, IEnumerable<UserProfile>>
    {
        private readonly DataContext _dbContext;

        public GetAllUserProfilesQueryHandler(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<UserProfile>> Handle(GetAllUserProfilesQuery request, CancellationToken cancellationToken)
        {
            var users = await _dbContext.UserProfiles.ToListAsync();

            return users;

        }
    }
}
