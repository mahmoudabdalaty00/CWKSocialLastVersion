using Application.Models;
using Application.Service.DTOs.PostDto;
using MediatR;

namespace Application.Features.PostsFeatures.Posts.Commands
{
    public class DeletePostCommand : IRequest<OperationResult<PostResponseDto>>
    {
        public string Id { get; set; }
    }
}
