using Application.Features.UserProfiles.Queries;
using Application.Models;
using Application.Service.DTOs.UserProfileDto;
using Application.Service.Interface.Services.UserProfileServices;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.UserProfiles.QueryHandlers
{
    public class GetUserProfileByIdQueryHandler : IRequestHandler<GetUserProfileByIdQuery, OperationResult<UserProfileResponseDto>>
    {
        private readonly IUserProfileService _userProfileService;

        public GetUserProfileByIdQueryHandler(IUserProfileService userProfileService)
        {
            _userProfileService = userProfileService;
        }

        public async Task<OperationResult<UserProfileResponseDto>> Handle(GetUserProfileByIdQuery request, CancellationToken cancellationToken)
        {
            return await _userProfileService.GetByIdAsync(request.UserProfileId);
        }
    }
}
