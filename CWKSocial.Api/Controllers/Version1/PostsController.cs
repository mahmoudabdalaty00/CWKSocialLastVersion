using Asp.Versioning;
using CWKSocial.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CWKSocial.Api.Controllers.Version1
{

    [ApiVersion("1.0")]//here we are specifying the version of the api
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : Controller
    {

        //we can use the MapToApiVersion attribute to specify the version of the api
        // [MapToApiVersion("1.0")]
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetPostById(int id)
        {
            return Ok(new Post { Id = id, Text = "This is a post" });
        }
    }
}
