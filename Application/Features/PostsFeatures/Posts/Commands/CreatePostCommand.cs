using Application.Models;
using Application.Service.DTOs.PostDto;
using Domain.Models.Conasts;
using MediatR;

namespace Application.Features.PostsFeatures.Posts.Commands
{
    public class CreatePostCommand : IRequest<OperationResult<PostResponseDto>>
    {
        public string Content { get; set; } = string.Empty;
        public string MediaUrl { get; set; } = string.Empty;
        public PostType PostType { get; set; } = PostType.Text;
        public PrivacySetting PrivacySetting { get; set; } = PrivacySetting.Public;
        public Guid UserProfileId { get; set; }
    }
}
