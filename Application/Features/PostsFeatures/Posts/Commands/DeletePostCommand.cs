using Application.DTOs.PostDto;
using Application.Models;
using MediatR;

namespace Application.Features.PostsFeatures.Posts.Commands
{
    public class DeletePostCommand : IRequest<OperationResult<PostResponseDto>>
    {
        public string Id { get; set; }
    }
}
