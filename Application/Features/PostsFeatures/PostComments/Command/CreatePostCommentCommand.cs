using Application.DTOs.PostCommentDto;
using Application.Models;
using MediatR;

namespace Application.Features.PostsFeatures.PostComments.Command
{
    public class CreatePostCommentCommand : IRequest<OperationResult<PostCommentResponseDto>>
    {
        public string PostId { get; set; }
        public string Text { get; set; } = string.Empty;
        public string CreatedById { get; set; }
    }
}
