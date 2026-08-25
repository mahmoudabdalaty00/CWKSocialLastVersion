using Application.Features.UserProfiles.Commands;
using Application.Models;
using AutoMapper;
using Data.MainDb;
using Domain.Models.Conasts;
using Domain.Models.UserProfiles;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserProfiles.CommandHandlers
{
    public class CreateUserProfileCommandHandler : IRequestHandler<CreateUserProfileCommand,OperationResult<UserProfile>>
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;

        public CreateUserProfileCommandHandler(DataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<OperationResult<UserProfile>> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken)
        {

            var result = new OperationResult<UserProfile>();
            try
            {
                var basicInfo = BasicInfo.Create(
                request.FirstName, request.LastName,
                        request.DateOfBirth, request.Bio,
                        request.Phone, request.EmailAddress, request.CurrentCity);


                var userProfile = UserProfile.Create(Guid.NewGuid().ToString(), basicInfo);

                _dbContext.UserProfiles.Add(userProfile);
                await _dbContext.SaveChangesAsync();
                var res = new OperationResult<UserProfile>();
                res.Result = userProfile;
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
    }
}
