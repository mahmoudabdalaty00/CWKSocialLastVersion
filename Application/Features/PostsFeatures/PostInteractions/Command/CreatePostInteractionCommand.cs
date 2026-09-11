using Application.Models;
using Application.Service.DTOs.PostInterActionDto;
using Domain.Models.Conasts;
using MediatR;

namespace Application.Features.PostsFeatures.PostInteractions.Command
{
    public class CreatePostInteractionCommand : IRequest<OperationResult<PostInterActionResponseDto>>
    {
        public int PostId { get; set; }
        public ReactionType ReactionType { get; set; }
    }
}
