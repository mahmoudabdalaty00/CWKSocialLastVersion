using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
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
