using Application.Features.UserProfiles.Commands;
using Application.Models;
using Application.Service.DTOs.UserProfileDto;
using Application.Service.Interface.Services.UserProfileServices;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.UserProfiles.CommandHandlers
{
    public class CreateUserProfileCommandHandler : IRequestHandler<CreateUserProfileCommand, OperationResult<UserProfileResponseDto>>
    {
        private readonly IUserProfileService _userProfileService;

        public CreateUserProfileCommandHandler(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        public async Task<OperationResult<UserProfileResponseDto>> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var dto = new CreateUserProfileDto
            {
                IdentityUserId = "", 
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                Bio = request.Bio,
                Phone = request.Phone,
                EmailAddress = request.EmailAddress,
                CurrentCity = request.CurrentCity
            };

            return await _userProfileService.CreateAsync(dto);
        }
    }
}
