using Application.DTOs.PostInterActionDto;
using Application.Models;
using Domain.Models.Conasts;
using MediatR;

namespace Application.Features.PostsFeatures.PostInteractions.Command
{
    public class UpdatePostInteractionCommand : IRequest<OperationResult<PostInterActionResponseDto>>
    {
        public int Id { get; set; }
        public ReactionType ReactionType { get; set; }
    }
}
