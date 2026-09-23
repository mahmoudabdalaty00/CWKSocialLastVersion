using Application.DTOs.UserProfileDto;
using Application.Models;
using MediatR;

namespace Application.Features.UserProfiles.Commands
{
    public class DeleteUserProfileCommand : IRequest<OperationResult<UserProfileResponseDto>>
    {
        public string Id { get; set; }
    }
}
