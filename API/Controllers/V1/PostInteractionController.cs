using API.Contracts.Posts.Requests;
using API.Contracts.Posts.Responses;
using API.Filters;
using API.Routes;
using Application.Features.PostsFeatures.PostInteractions.Command;
using Application.Features.PostsFeatures.PostInteractions.Queries;
using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class PostInteractionController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PostInteractionController(IMapper mapper, IMediator mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpGet(ApiRoutes.PostInteraction.GetByPost)]
        public async Task<IActionResult> GetInteractionsByPost(int postId)
        {
            var query = new GetPostInteractionsByPostIdQuery { PostId = postId };
            var response = await _mediator.Send(query);

            if (response.IsError)
                return HandlerErrorResponse(response.Errors);

            return Ok(_mapper.Map<List<PostInteractionResponse>>(response.Result));
        }

        [HttpPost(ApiRoutes.PostInteraction.Create)]
        [ValidateModel]
        public async Task<IActionResult> CreatePostInteraction([FromBody] PostInteractionCreate request)
        {
            var command = _mapper.Map<CreatePostInteractionCommand>(request);
            var response = await _mediator.Send(command);

            if (response.IsError)
                return HandlerErrorResponse(response.Errors);

            return Ok(_mapper.Map<PostInteractionResponse>(response.Result));
        }

        [HttpPatch(ApiRoutes.PostInteraction.Update + "/" + ApiRoutes.PostInteraction.IdRoute)]
        [ValidateModel]
        public async Task<IActionResult> UpdatePostInteraction(int id, [FromBody] PostInteractionUpdate request)
        {
            var command = _mapper.Map<UpdatePostInteractionCommand>(request);
            command.Id = id;
            var response = await _mediator.Send(command);

            return response.IsError ? HandlerErrorResponse(response.Errors) : Ok(response);
        }

        [HttpDelete(ApiRoutes.PostInteraction.Delete + "/" + ApiRoutes.PostInteraction.IdRoute)]
        public async Task<IActionResult> DeletePostInteraction(int id)
        {
            var command = new DeletePostInteractionCommand { Id = id };
            var response = await _mediator.Send(command);

            if (response.IsError)
                return HandlerErrorResponse(response.Errors);

            return NoContent();
        }
    }
}
