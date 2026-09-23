using Application.DTOs.UserProfileDto;
using Application.Models;
using MediatR;

namespace Application.Features.UserProfiles.Commands
{
    public class CreateUserProfileCommand : IRequest<OperationResult<UserProfileResponseDto>>
    { 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Bio { get; set; }
        public string Phone { get; set; }
        public string CurrentCity { get; set; }
        public DateOnly DateOfBirth { get; set; }
    }
}
