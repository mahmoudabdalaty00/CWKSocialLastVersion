using Application.Models;
using Application.Service.DTOs.PostInterActionDto;
using MediatR;

namespace Application.Features.PostsFeatures.PostInteractions.Command
{
    public class DeletePostInteractionCommand : IRequest<OperationResult<PostInterActionResponseDto>>
    {
        public int Id { get; set; }
    }
}
