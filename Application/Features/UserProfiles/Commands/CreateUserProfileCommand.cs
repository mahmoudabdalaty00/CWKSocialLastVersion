using Application.Models;
using Domain.Models.UserProfiles;
using MediatR;

namespace Application.Features.UserProfiles.Commands
{
    public class CreateUserProfileCommand : IRequest<OperationResult<UserProfile>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Bio { get; set; }
        public string Phone { get; set; }
        public string CurrentCity { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
