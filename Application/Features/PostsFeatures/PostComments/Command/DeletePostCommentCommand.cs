using Application.Models;
using Application.Service.DTOs.PostCommentDto;
using MediatR;

namespace Application.Features.PostsFeatures.PostComments.Command
{
    public class DeletePostCommentCommand : IRequest<OperationResult<PostCommentResponseDto>>
    {
        public Guid Id { get; set; }
    }
}
