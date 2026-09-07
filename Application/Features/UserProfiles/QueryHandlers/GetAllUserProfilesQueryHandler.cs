using Application.Features.UserProfiles.Queries;
using Application.Models;
using Application.Service.DTOs.UserProfileDto;
using Application.Service.Interface.Services.UserProfileServices;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.UserProfiles.QueryHandlers
{
    public class GetAllUserProfilesQueryHandler : IRequestHandler<GetAllUserProfilesQuery, OperationResult<IEnumerable<UserProfileResponseDto>>>
    {
        private readonly IUserProfileService _userProfileService;

        public GetAllUserProfilesQueryHandler(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        public async Task<OperationResult<IEnumerable<UserProfileResponseDto>>> Handle(GetAllUserProfilesQuery request, CancellationToken cancellationToken)
        {
            return await _userProfileService.GetAllAsync();
        }
    }
}
