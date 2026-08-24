using API.Contracts.Common;
using API.Contracts.UserProfile.Requests;
using API.Contracts.UserProfile.Responses;
using API.Routes;
using Application.Features.UserProfiles.Commands;
using Application.Features.UserProfiles.Queries;
using Asp.Versioning;
using AutoMapper;
using Domain.Models.Conasts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class UserProfileController : BaseController
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

            if (response == null)
                return NotFound();

            var userProfilesResponse = _mapper.Map<List<UserProfileResponse>>(response);
            return Ok(userProfilesResponse);
        }




        [HttpGet(ApiRoutes.UserProfiles.GetUserProfile + "/" + ApiRoutes.UserProfiles.IdRoute)]
        public async Task<IActionResult> GetUserProfileById(string id)
        {
            var query = new GetUserProfileByIdQuery { UserProfileId = Guid.Parse(id) };
            var response = await _mediator.Send(query);

            if (response == null)
                return NotFound();

            var user = _mapper.Map<UserProfileResponse>(response);
            return Ok(user);
        }



        [HttpPost(ApiRoutes.UserProfiles.CreateUserProfile)]
        public async Task<IActionResult> CreateUserProfile([FromBody] UserProfileCreate userProfile)
        {
            var command = _mapper.Map<CreateUserProfileCommand>(userProfile);
            // Handle the command and return the result
            var response = await _mediator.Send(command);

            if (response == null)
                return NotFound();

            var userProfileResponse = _mapper.Map<UserProfileResponse>(response);
            return CreatedAtAction(
                nameof(GetUserProfileById), new { userProfileResponse.Id }, userProfileResponse);
        }


        [HttpPatch(ApiRoutes.UserProfiles.UpdateUserProfile + "/" + ApiRoutes.UserProfiles.IdRoute)]
        public async Task<IActionResult> UpdateUserProfile(string id, [FromBody] UserProfileUpdate userProfile)
        {
            var command = _mapper.Map<UpdateUserProfileCommand>(userProfile);

            command.Id = Guid.Parse(id);
            // Handle the command and return the result
            var response = await _mediator.Send(command);
             
            return response.IsError ? HandlerErrorResponse(response.Errors) : Ok(response);
        }






        [HttpDelete(ApiRoutes.UserProfiles.DeleteUserProfile + "/" + ApiRoutes.UserProfiles.IdRoute)]
        public async Task<IActionResult> DeleteUserProfile(string id)
        {
            var command = new DeleteUserProfileCommand { Id = Guid.Parse(id) };

            if (command == null)
                return NotFound();

            await _mediator.Send(command);
            return NoContent();
        }

         
    }
}
