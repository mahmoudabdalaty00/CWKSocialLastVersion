using Application.DTOs.PostCommentDto;
using Application.Models;
using MediatR;

namespace Application.Features.PostsFeatures.PostComments.Command
{
    public class UpdatePostCommentCommand : IRequest<OperationResult<PostCommentResponseDto>>
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
