using API.Contracts.UserProfile.Requests;
using API.Routes;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class UserProfileController : Controller
    {
        private readonly IMediator _mediator;
        public UserProfileController( IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProfiles()
        {
            return (IActionResult)Task.FromResult(Ok());
        }


        [HttpPost]
        public async Task<IActionResult> CreateUserProfile([FromBody]UserProfileCreate userProfile)
        {
            return (IActionResult)Task.FromResult(Ok());
        }








    }
}

