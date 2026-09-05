using AutoMapper;
using Domain.Models.Posts;
using Application.Service.DTOs.PostDto;
using Application.Service.DTOs.PostCommentDto;
using Application.Service.DTOs.PostInterActionDto;

namespace Application.AutoMapper
{
    public class PostMappingProfile : Profile
    {
        public PostMappingProfile()
        {
            // Post Mappings
            CreateMap<Post, PostResponseDto>();

            CreateMap<CreatePostDto, Post>()
                .ConstructUsing((dto, context) => Post.Create(
                    dto.Content,
                    dto.MediaUrl,
                    dto.PostType,
                    dto.PrivacySetting,
                    dto.UserProfileId))
                .ForAllMembers(opts => opts.Ignore());

            // PostComment Mappings
            CreateMap<PostComment, PostCommentResponseDto>();

            CreateMap<CreatePostCommentDto, PostComment>()
                .ConstructUsing((dto, context) => PostComment.Create(
                    dto.PostId,
                    dto.Text,
                    dto.UserProfileId))
                .ForAllMembers(opts => opts.Ignore());

            // PostInterAction Mappings
            CreateMap<PostInterAction, PostInterActionResponseDto>();

            CreateMap<CreatePostInterActionDto, PostInterAction>()
                .ConstructUsing((dto, context) => PostInterAction.Create(
                    dto.PostId,
                    dto.ReactionType))
                .ForAllMembers(opts => opts.Ignore());
        }
    }
}
