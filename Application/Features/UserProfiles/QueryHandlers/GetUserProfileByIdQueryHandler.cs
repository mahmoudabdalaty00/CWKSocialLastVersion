using Application.Features.UserProfiles.Queries;
using Application.Models;
using Data.MainDb;
using Domain.Models.Conasts;
using Domain.Models.UserProfiles;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserProfiles.QueryHandlers
{
    public class GetUserProfileByIdQueryHandler : IRequestHandler<GetUserProfileByIdQuery, OperationResult<UserProfile>>
    {
        private readonly DataContext _db;

        public GetUserProfileByIdQueryHandler(DataContext db)
        {
            _db = db;
        }

        public async Task<OperationResult<UserProfile>> Handle(GetUserProfileByIdQuery request, CancellationToken cancellationToken)
        {
            var user =await _db.UserProfiles
                .FirstOrDefaultAsync(u => u.Id == request.UserProfileId&& !u.IsDeleted); 
            var result = new OperationResult<UserProfile>();
            if(user == null)
            {
                var error = new Error
                {
                    Code = ErrorCodes.NotFound,
                    Message = $"User profile not found With UserId : {request.UserProfileId}.",
                };
                result.Result = null;
                result.IsError = true;
                result.Errors.Add(error);
                return result;
            }
            result.Result = user;
            result.IsError = false;
            return result;
        }
    }
}
