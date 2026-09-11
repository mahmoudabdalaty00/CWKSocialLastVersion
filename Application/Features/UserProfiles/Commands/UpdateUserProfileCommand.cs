using Application.Models;
using Domain.Models.UserProfiles;
using MediatR;
using System;
using System.Collections.Generic;
using Application.Service.DTOs.UserProfileDto;
using System.Text;

namespace Application.Features.UserProfiles.Commands
{
    public class UpdateUserProfileCommand : IRequest<OperationResult<UserProfileResponseDto>>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string Bio { get; set; }
        public string Phone { get; set; }
        public string CurrentCity { get; set; }
        public DateOnly DateOfBirth { get; set; }
    }
}
