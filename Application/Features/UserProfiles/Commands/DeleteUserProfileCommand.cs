using MediatR;

namespace Application.Features.UserProfiles.Commands
{
    public class DeleteUserProfileCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
