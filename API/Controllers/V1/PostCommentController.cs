using API.Contracts.Posts.Requests;
using API.Contracts.Posts.Responses;
using API.Filters;
using API.Routes;
using Application.Features.PostsFeatures.PostComments.Command;
using Application.Features.PostsFeatures.PostComments.Queries;
using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class PostCommentController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PostCommentController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet(ApiRoutes.PostComment.GetByPost)]
        public async Task<IActionResult> GetCommentsByPost(int postId)
        {
            var query = new GetPostCommentsByPostIdQuery { PostId = postId };
            var response = await _mediator.Send(query);

            if (response.IsError)
                return HandlerErrorResponse(response.Errors);

            return Ok(_mapper.Map<List<PostCommentResponse>>(response.Result));
        }

        [HttpPost(ApiRoutes.PostComment.Create)]
        [ValidateModel]
        public async Task<IActionResult> CreatePostComment([FromBody] PostCommentCreate request)
        {
            var command = _mapper.Map<CreatePostCommentCommand>(request);
            var response = await _mediator.Send(command);

            if (response.IsError)
                return HandlerErrorResponse(response.Errors);

            return Ok(_mapper.Map<PostCommentResponse>(response.Result));
        }

        [HttpPatch(ApiRoutes.PostComment.Update + "/" + ApiRoutes.PostComment.IdRoute)]
        [ValidateModel]
        public async Task<IActionResult> UpdatePostComment(int id, [FromBody] PostCommentUpdate request)
        {
            var command = _mapper.Map<UpdatePostCommentCommand>(request);
            command.Id = id;
            var response = await _mediator.Send(command);

            return response.IsError ? HandlerErrorResponse(response.Errors) : Ok(response);
        }

        [HttpDelete(ApiRoutes.PostComment.Delete + "/" + ApiRoutes.PostComment.IdRoute)]
        public async Task<IActionResult> DeletePostComment(int id)
        {
            var command = new DeletePostCommentCommand { Id = id };
            var response = await _mediator.Send(command);

            if (response.IsError)
                return HandlerErrorResponse(response.Errors);

            return NoContent();
        }
    }
}
