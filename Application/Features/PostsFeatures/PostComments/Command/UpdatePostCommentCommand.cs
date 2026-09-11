using Application.Models;
using Application.Service.DTOs.PostCommentDto;
using MediatR;

namespace Application.Features.PostsFeatures.PostComments.Command
{
    public class UpdatePostCommentCommand : IRequest<OperationResult<PostCommentResponseDto>>
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}
