using Application.Features.UserProfiles.Commands;
using Application.Models;
using Application.Service.DTOs.UserProfileDto;
using Application.Service.Interface.Services.UserProfileServices;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.UserProfiles.CommandHandlers
{
    public class DeleteUserProfileCommandHandler : IRequestHandler<DeleteUserProfileCommand, OperationResult<UserProfileResponseDto>>
    {
        private readonly IUserProfileService _userProfileService;

        public DeleteUserProfileCommandHandler(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        public async Task<OperationResult<UserProfileResponseDto>> Handle(DeleteUserProfileCommand request, CancellationToken cancellationToken)
        {
            return await _userProfileService.DeleteAsync(request.Id);
        }
    }
}
