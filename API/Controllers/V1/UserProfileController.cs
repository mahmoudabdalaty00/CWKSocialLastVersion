using API.Contracts.UserProfile.Requests;
using API.Contracts.UserProfile.Responses;
using API.Routes;
using Application.UserProfiles.Commands;
using Application.UserProfiles.Queries;
using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class UserProfileController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public UserProfileController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet(ApiRoutes.UserProfiles.GetAllUsers)]
        public async Task<IActionResult> GetAllProfiles()
        {
            var query = new GetAllUserProfilesQuery();
            var response = await _mediator.Send(query);
            var userProfilesResponse = _mapper.Map<List<UserProfileResponse>>(response);
            return Ok(userProfilesResponse);
        }



        [HttpPost(ApiRoutes.UserProfiles.CreateUserProfile)]
        public async Task<IActionResult> CreateUserProfile([FromBody] UserProfileCreate userProfile)
        {
            var command = _mapper.Map<CreateUserProfileCommand>(userProfile);
            // Handle the command and return the result
            var response = await _mediator.Send(command);

            var userProfileResponse = _mapper.Map<UserProfileResponse>(response);
            return CreatedAtAction(
                nameof(GetUserProfileById), new { userProfileResponse.Id }, userProfileResponse);
        }



        [HttpGet(ApiRoutes.UserProfiles.GetUserProfile + "/" + ApiRoutes.UserProfiles.IdRoute)]
        public async Task<IActionResult> GetUserProfileById(string id)
        {
            var query = new GetUserProfileByIdQuery { UserProfileId = Guid.Parse(id) };
            var response =await _mediator.Send(query);
            var user = _mapper.Map<UserProfileResponse>(response);
            return Ok(user);
        }



    }
}
