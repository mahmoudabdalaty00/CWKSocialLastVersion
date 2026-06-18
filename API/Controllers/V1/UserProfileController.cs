using API.Routes;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class UserProfileController : Controller
    {
        public UserProfileController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProfiles()
        {
            return (IActionResult)Task.FromResult(Ok());
        }


        public async Task<IActionResult> CreateUserProfile()
        {
            return (IActionResult)Task.FromResult(Ok());
        }








    }
}
