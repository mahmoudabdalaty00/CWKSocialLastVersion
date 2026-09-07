using Application.Models;
using Application.Service.DTOs.PostCommentDto;
using MediatR;

namespace Application.Features.PostsFeatures.PostComments.Queries
{
    public class GetPostCommentsByPostIdQuery : IRequest<OperationResult<IEnumerable<PostCommentResponseDto>>>
    {
        public Guid PostId { get; set; }
    }
}
