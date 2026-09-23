using Application.DTOs.PostCommentDto;
using Application.Models;
using MediatR;

namespace Application.Features.PostsFeatures.PostComments.Command
{
    public class DeletePostCommentCommand : IRequest<OperationResult<PostCommentResponseDto>>
    {
        public int Id { get; set; }
    }
}
