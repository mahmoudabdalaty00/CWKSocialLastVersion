using Application.Models;
using Application.Service.DTOs.PostInterActionDto;
using Domain.Models.Conasts;
using MediatR;

namespace Application.Features.PostsFeatures.PostInteractions.Command
{
    public class UpdatePostInteractionCommand : IRequest<OperationResult<PostInterActionResponseDto>>
    {
        public Guid Id { get; set; }
        public ReactionType ReactionType { get; set; }
    }
}
