using Application.Features.UserProfiles.Queries;
using Application.Models;
using Data.MainDb;
using Domain.Models.UserProfiles;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens.Experimental;

namespace Application.Features.UserProfiles.QueryHandlers
{
    public class GetAllUserProfilesQueryHandler : IRequestHandler<GetAllUserProfilesQuery, OperationResult<IEnumerable<UserProfile>>>
    {
        private readonly DataContext _dbContext;

        public GetAllUserProfilesQueryHandler(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<IEnumerable<UserProfile>>> Handle(GetAllUserProfilesQuery request, CancellationToken cancellationToken)
        {
            var users = await _dbContext.UserProfiles.ToListAsync();
            var result = new OperationResult<IEnumerable<UserProfile>>
            {
                Result = users,
                IsError = false,
                Errors = new List<Error>()
            };
            return result;

        }
    }
}
