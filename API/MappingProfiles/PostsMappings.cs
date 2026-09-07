using API.Contracts.Posts.Requests;
using API.Contracts.Posts.Responses;
using Application.Features.PostsFeatures.PostComments.Command;
using Application.Features.PostsFeatures.PostInteractions.Command;
using Application.Features.PostsFeatures.Posts.Commands;
using Application.Service.DTOs.PostDto;
using Application.Service.DTOs.PostCommentDto;
using Application.Service.DTOs.PostInterActionDto;
using AutoMapper;

namespace API.MappingProfiles
{
    public class PostsMappings : Profile
    {
        public PostsMappings()
        {
            // API request contracts → Commands
            CreateMap<PostCreate, CreatePostCommand>();
            CreateMap<PostUpdate, UpdatePostCommand>();

            CreateMap<PostCommentCreate, CreatePostCommentCommand>();
            CreateMap<PostCommentUpdate, UpdatePostCommentCommand>();

            CreateMap<PostInteractionCreate, CreatePostInteractionCommand>();
            CreateMap<PostInteractionUpdate, UpdatePostInteractionCommand>();

            // Application DTOs → API response contracts
            CreateMap<PostResponseDto, PostResponse>();
            CreateMap<PostCommentResponseDto, PostCommentResponse>();
            CreateMap<PostInterActionResponseDto, PostInteractionResponse>();
        }
    }
}
