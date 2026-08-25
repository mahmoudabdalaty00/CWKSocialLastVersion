using Application.Models;
using Domain.Models.UserProfiles;
using MediatR;

namespace Application.Features.UserProfiles.Commands
{
    public class DeleteUserProfileCommand : IRequest<OperationResult<UserProfile>>
    {
        public Guid Id { get; set; }
    }
}
