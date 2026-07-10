using API.Routes;
using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V2
{
    [ApiVersion("2.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class PostController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPosts()
        {
            // Logic to retrieve posts
            return Ok("Get all posts");
        }

        [HttpGet("{id}")]
        public IActionResult GetPostById(Guid id)
        {
            // Logic to retrieve a post by id
            return Ok($"Get post with id: {id}");
        }
    }
}
 
 