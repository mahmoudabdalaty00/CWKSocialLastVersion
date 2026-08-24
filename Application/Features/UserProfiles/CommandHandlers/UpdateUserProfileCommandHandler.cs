using Application.Features.UserProfiles.Commands;
using Application.Models;
using Data.MainDb;
using Domain.Models.Conasts;
using Domain.Models.UserProfiles;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserProfiles.CommandHandlers
{
    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, OperationResult<UserProfile>>
    {
        private readonly DataContext _db;

        public UpdateUserProfileCommandHandler(DataContext db)
        {
            _db = db;
        }

        public async Task<OperationResult<UserProfile>> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {

            var result = new OperationResult<UserProfile>();
            try
            {
                var userProfile = await _db.UserProfiles.FirstOrDefaultAsync(up =>
                    up.Id == request.Id);


                var basicInfo = BasicInfo.Create(
                    request.FirstName, request.LastName,
                    request.DateOfBirth,
                    request.Bio,
                    request.Phone,
                    request.EmailAddress,
                    request.CurrentCity);

                if (userProfile == null)
                {
                    var error = new Error
                    {
                        Code =  ErrorCodes.NotFound,
                        Message = $"User profile not found With UserId : {request.Id}.",
                    };
                    result.Result = null;
                    result.IsError = true;
                    result.Errors.Add(error);
                    return result;
                }


                userProfile.UpdateBasicInfo(basicInfo);

                userProfile.UpdatedAt = DateTime.UtcNow;
                _db.UserProfiles.Update(userProfile);
                await _db.SaveChangesAsync(cancellationToken);

                result.Result = userProfile;
                result.IsError = false;


            }
            catch (Exception ex)
            {
                var error = new Error
                {
                    Code = ErrorCodes.ServerError,
                    Message = ex.Message,
                };
                result.Errors.Add(error);
                result.IsError = true;
                result.Result = null;
            }

            return result;

        }
    }
}
