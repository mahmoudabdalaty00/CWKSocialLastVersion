using Application.DTOs.PostCommentDto;
using Application.DTOs.PostDto;
using Application.DTOs.PostInterActionDto;
using Application.Features.PostsFeatures.Posts.Commands;
using AutoMapper;
using Domain.Models.Posts;

namespace Application.AutoMapper
{
    public class PostMappingProfile : Profile
    {
        public PostMappingProfile()
        {
            // Post Mappings
            CreateMap<Post, PostResponseDto>();
            CreateMap<CreatePostCommand, CreatePostDto>();

            CreateMap<CreatePostDto, Post>()
                .ConstructUsing((dto, context) => Post.Create(
                    dto.Content,
                    dto.MediaUrl,
                    dto.PostType,
                    dto.PrivacySetting,
                    dto.CreatedById))
                .ForAllMembers(opts => opts.Ignore());



            // PostComment Mappings
            CreateMap<PostComment, PostCommentResponseDto>();

            CreateMap<CreatePostCommentDto, PostComment>()
                .ConstructUsing((dto, context) => PostComment.Create(
                    dto.PostId,
                    dto.Text,
                    dto.CreatedById))
                .ForAllMembers(opts => opts.Ignore());

            // PostInterAction Mappings
            CreateMap<PostInterAction, PostInterActionResponseDto>();

            CreateMap<CreatePostInterActionDto, PostInterAction>()
                .ConstructUsing((dto, context) => PostInterAction.Create(
                    dto.PostId,
                    dto.ReactionType,
                    dto.CreatedById))
                .ForAllMembers(opts => opts.Ignore());
        }
    }
}
