using Application.DTOs.PostDto;
using Application.Models;
using Domain.Models.Conasts;
using MediatR;

namespace Application.Features.PostsFeatures.Posts.Commands
{
    public class UpdatePostCommand : IRequest<OperationResult<PostResponseDto>>
    {
        public string Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public string MediaUrl { get; set; } = string.Empty;
        public PostType PostType { get; set; } = PostType.Text;
        public PrivacySetting PrivacySetting { get; set; } = PrivacySetting.Public;
    }
}
