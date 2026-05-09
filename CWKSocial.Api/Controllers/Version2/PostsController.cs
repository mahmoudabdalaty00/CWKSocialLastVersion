using Asp.Versioning;
using CWKSocial.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CWKSocial.Api.Controllers.Version2
{
    [ApiVersion("2.0")]
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : Controller
    {

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetPostById(int id)
        {
            return Ok(new Post { Id = id, Text = "This is a post" });
        }
    }
}
