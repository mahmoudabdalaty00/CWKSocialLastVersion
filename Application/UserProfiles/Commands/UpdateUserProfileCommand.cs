using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UserProfiles.Commands
{
    public class UpdateUserProfileCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Bio { get; set; }
        public string Phone { get; set; }
        public string CurrentCity { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
