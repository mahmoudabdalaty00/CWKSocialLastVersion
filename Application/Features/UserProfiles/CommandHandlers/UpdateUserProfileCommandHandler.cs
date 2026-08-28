using Application.Features.UserProfiles.Commands;
using Application.Models;
using Data.MainDb;
using Domain.Models.Conasts;
using Domain.Models.UserProfiles;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserProfiles.CommandHandlers
{
    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, OperationResult<UserProfile>>
    {
        private readonly DataContext _db;
        private readonly IValidator<UpdateUserProfileCommand> _validator;

        public UpdateUserProfileCommandHandler(DataContext db, IValidator<UpdateUserProfileCommand> validator)
        {
            _db = db;
            _validator = validator;
        }

        public async Task<OperationResult<UserProfile>> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {

            var result = new OperationResult<UserProfile>();
            // 1. Run the validator directly
            (bool flowControl, OperationResult<UserProfile> value) = await Validate(request, result, cancellationToken);
            if (!flowControl)
            {
                return value;
            }
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
                        Code = ErrorCodes.NotFound,
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

        private async Task<(bool flowControl, OperationResult<UserProfile> value)> Validate(UpdateUserProfileCommand request, OperationResult<UserProfile> result, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            // 2. Check if validation failed
            if (!validationResult.IsValid)
            {
                result.IsError = true;
                foreach (var failure in validationResult.Errors)
                {
                    result.Errors.Add(new Error
                    {
                        Code = ErrorCodes.ValidationError,
                        Message = failure.ErrorMessage
                    });
                }

                return (flowControl: false, value: result); // Stop execution early
            }

            return (flowControl: true, value: null);
        }
    }
}
