using Application.DTOs.PostInterActionDto;
using Application.Models;
using Domain.Models.Conasts;
using MediatR;

namespace Application.Features.PostsFeatures.PostInteractions.Command
{
    public class CreatePostInteractionCommand : IRequest<OperationResult<PostInterActionResponseDto>>
    {
        public string PostId { get; set; }
        public string CreatedById { get; set; }
        public ReactionType ReactionType { get; set; }
    }
}
