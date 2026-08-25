using Application.Features.UserProfiles.Commands;
using Application.Models;
using Data.MainDb;
using Domain.Models.Conasts;
using Domain.Models.UserProfiles;
using MediatR;

namespace Application.Features.UserProfiles.CommandHandlers
{
    public class DeleteUserProfileCommandHandler : IRequestHandler<DeleteUserProfileCommand, OperationResult<UserProfile>>
    {
        private readonly DataContext _db;

        public DeleteUserProfileCommandHandler(DataContext db)
        {
            _db = db;
        }

        public async Task<OperationResult<UserProfile>> Handle(DeleteUserProfileCommand request, CancellationToken cancellationToken)
        {
            var profile = _db.UserProfiles.FirstOrDefault(x => x.Id == request.Id);

            var result = new OperationResult<UserProfile>();
            if(profile == null) 
            {
                var error = new Error
                {
                    Code = ErrorCodes.NotFound,
                    Message = $"User profile not found With UserId : {request.Id}.",
                };
                result.Result = null;
                result.IsError = true;
                result.Errors.Add(error);
                return result;
            }


            profile.IsDeleted = true;
            profile.DeletedAt = DateTime.UtcNow;

            _db.UserProfiles.Update(profile);
            await  _db.SaveChangesAsync();

            result.Result = profile;
            result.IsError = false;
            return result;
        }
    }
}
