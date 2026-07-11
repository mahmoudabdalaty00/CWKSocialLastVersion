using MediatR;

namespace Application.UserProfiles.Commands
{
    public class DeleteUserProfileCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
