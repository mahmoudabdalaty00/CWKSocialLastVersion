using Application.Features.UserProfiles.Commands;
using Application.Models;
using AutoMapper;
using Data.MainDb;
using Domain.Models.Conasts;
using Domain.Models.UserProfiles;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserProfiles.CommandHandlers
{
    public class CreateUserProfileCommandHandler : IRequestHandler<CreateUserProfileCommand, OperationResult<UserProfile>>
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateUserProfileCommand> _validator;

        public CreateUserProfileCommandHandler(DataContext dbContext, IMapper mapper, IValidator<CreateUserProfileCommand> validator)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<OperationResult<UserProfile>> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken)
        {

            var result = new OperationResult<UserProfile>();
            (bool flowControl, OperationResult<UserProfile> value) = await validate(request, result, cancellationToken);
            if (!flowControl)
            {
                return value;
            }

            try
            {
                var basicInfo = BasicInfo.Create(
                request.FirstName, request.LastName,
                        request.DateOfBirth, request.Bio,
                        request.Phone, request.EmailAddress, request.CurrentCity);


                var userProfile = UserProfile.Create(Guid.NewGuid().ToString(), basicInfo);

                _dbContext.UserProfiles.Add(userProfile);
                await _dbContext.SaveChangesAsync();
                result.Result = userProfile;
                result.IsError = false;
            }
            catch (DbUpdateException ex)
            {
                result.IsError = true;
                result.Errors.Add(new Error { Code = ErrorCodes.DbError, Message = ex.Message });
            }
            catch (ArgumentException ex) // BasicInfo.Create validation failures
            {
                result.IsError = true;
                result.Errors.Add(new Error { Code = ErrorCodes.ValidationError, Message = ex.Message });
            }

            return result;
        }

        private async Task<(bool flowControl, OperationResult<UserProfile> value)> validate(CreateUserProfileCommand request, OperationResult<UserProfile> result, CancellationToken cancellationToken)
        {
            // 1. Run the validator directly
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

                return (flowControl: false, value: result); // Stop execution here and return the errors
            }

            return (flowControl: true, value: null);
        }
    }
}
