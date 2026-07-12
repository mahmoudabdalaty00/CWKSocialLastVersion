using Application.Features.UserProfiles.Queries;
using Data.MainDb;
using Domain.Models.UserProfiles;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserProfiles.QueryHandlers
{
    public class GetUserProfileByIdQueryHandler : IRequestHandler<GetUserProfileByIdQuery, UserProfile>
    {
        private readonly DataContext _db;

        public GetUserProfileByIdQueryHandler(DataContext db)
        {
            _db = db;
        }

        public async Task<UserProfile> Handle(GetUserProfileByIdQuery request, CancellationToken cancellationToken)
        {
            var user =await _db.UserProfiles.FirstOrDefaultAsync(u => u.Id == request.UserProfileId); 
            return user;
        }
    }
}
