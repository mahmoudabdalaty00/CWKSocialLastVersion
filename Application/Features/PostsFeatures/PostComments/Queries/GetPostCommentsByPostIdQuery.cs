using Application.DTOs.PostCommentDto;
using Application.Models;
using MediatR;

namespace Application.Features.PostsFeatures.PostComments.Queries
{
    public class GetPostCommentsByPostIdQuery : IRequest<OperationResult<IEnumerable<PostCommentResponseDto>>>
    {
        public string PostId { get; set; }
    }
}
