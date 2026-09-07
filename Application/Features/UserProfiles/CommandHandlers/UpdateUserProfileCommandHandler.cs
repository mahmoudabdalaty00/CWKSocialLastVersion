using Application.Features.UserProfiles.Commands;
using Application.Models;
using Application.Service.DTOs.UserProfileDto;
using Application.Service.Interface.Services.UserProfileServices;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.UserProfiles.CommandHandlers
{
    public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, OperationResult<UserProfileResponseDto>>
    {
        private readonly IUserProfileService _userProfileService;

        public UpdateUserProfileCommandHandler(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        public async Task<OperationResult<UserProfileResponseDto>> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
        {
            var dto = new UpdateUserProfileDto
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                Bio = request.Bio,
                Phone = request.Phone,
                EmailAddress = request.EmailAddress,
                CurrentCity = request.CurrentCity
            };

            return await _userProfileService.UpdateAsync(request.Id, dto);
        }
    }
}
