using Application.DTOs.PostInterActionDto;
using Application.Models;
using MediatR;

namespace Application.Features.PostsFeatures.PostInteractions.Queries
{
    public class GetPostInteractionsByPostIdQuery : IRequest<OperationResult<IEnumerable<PostInterActionResponseDto>>>
    {
        public string PostId { get; set; }
    }
}
