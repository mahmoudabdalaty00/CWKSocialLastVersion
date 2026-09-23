using Application.DTOs.PostInterActionDto;
using Application.Models;
using MediatR;

namespace Application.Features.PostsFeatures.PostInteractions.Command
{
    public class DeletePostInteractionCommand : IRequest<OperationResult<PostInterActionResponseDto>>
    {
        public int Id { get; set; }
    }
}
