using Application.Models;
using Application.Service.DTOs.UserProfileDto;
using MediatR;

namespace Application.Features.UserProfiles.Commands
{
    public class DeleteUserProfileCommand : IRequest<OperationResult<UserProfileResponseDto>>
    {
        public Guid Id { get; set; }
    }
}
