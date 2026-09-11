using Application.Models;
using Application.Service.DTOs.PostInterActionDto;
using MediatR;

namespace Application.Features.PostsFeatures.PostInteractions.Queries
{
    public class GetPostInteractionsByPostIdQuery : IRequest<OperationResult<IEnumerable<PostInterActionResponseDto>>>
    {
        public int PostId { get; set; }
    }
}
