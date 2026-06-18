using API.Routes;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1
{
    [ApiVersion("1.0")]
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

        [HttpGet(ApiRoutes.Post.GetById)]
        public IActionResult GetPostById(Guid id)
        {
            // Logic to retrieve a post by id
            return Ok($"Get post with id: {id}");
        }
    }
}
