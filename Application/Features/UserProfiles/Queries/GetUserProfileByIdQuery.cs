using Application.Models;
using Domain.Models.UserProfiles;
using MediatR;
using System;
using System.Collections.Generic;
using Application.Service.DTOs.UserProfileDto;
using System.Text;

namespace Application.Features.UserProfiles.Queries
{
    public class GetUserProfileByIdQuery :IRequest<OperationResult<UserProfileResponseDto>>
    {
        public string UserProfileId { get; set; }

    }
}
