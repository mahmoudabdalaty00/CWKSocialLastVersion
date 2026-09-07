using API.Contracts.Posts.Requests;
using API.Contracts.Posts.Responses;
using API.Filters;
using API.Routes;
using Application.Features.PostsFeatures.Posts.Commands;
using Application.Features.PostsFeatures.Posts.Queries;
using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class PostController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PostController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet(ApiRoutes.Post.GetAll)]
        public async Task<IActionResult> GetAllPosts()
        {
            var query = new GetAllPostsQuery();
            var response = await _mediator.Send(query);

            if (response.IsError)
                return HandlerErrorResponse(response.Errors);

            return Ok(_mapper.Map<List<PostResponse>>(response.Result));
        }

        [HttpGet(ApiRoutes.Post.GetById)]
        public async Task<IActionResult> GetPostById(string id)
        {
            var query = new GetPostByIdQuery { PostId = Guid.Parse(id) };
            var response = await _mediator.Send(query);

            if (response.IsError)
                return HandlerErrorResponse(response.Errors);

            return Ok(_mapper.Map<PostResponse>(response.Result));
        }

        [HttpPost(ApiRoutes.Post.Create)]
        [ValidateModel]
        public async Task<IActionResult> CreatePost([FromBody] PostCreate request)
        {
            var command = _mapper.Map<CreatePostCommand>(request);
            var response = await _mediator.Send(command);

            if (response.IsError)
                return HandlerErrorResponse(response.Errors);

            var postResponse = _mapper.Map<PostResponse>(response.Result);
            return CreatedAtAction(nameof(GetPostById), new { id = postResponse.Id }, postResponse);
        }

        [HttpPatch(ApiRoutes.Post.Update + "/" + ApiRoutes.Post.IdRoute)]
        [ValidateModel]
        public async Task<IActionResult> UpdatePost(string id, [FromBody] PostUpdate request)
        {
            var command = _mapper.Map<UpdatePostCommand>(request);
            command.Id = Guid.Parse(id);
            var response = await _mediator.Send(command);

            return response.IsError ? HandlerErrorResponse(response.Errors) : Ok(response);
        }

        [HttpDelete(ApiRoutes.Post.Delete + "/" + ApiRoutes.Post.IdRoute)]
        public async Task<IActionResult> DeletePost(string id)
        {
            var command = new DeletePostCommand { Id = Guid.Parse(id) };
            var response = await _mediator.Send(command);

            if (response.IsError)
                return HandlerErrorResponse(response.Errors);

            return NoContent();
        }
    }
}
