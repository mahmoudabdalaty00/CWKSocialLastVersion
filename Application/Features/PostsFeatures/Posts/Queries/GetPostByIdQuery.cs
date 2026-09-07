using Application.Models;
using Application.Service.DTOs.PostDto;
using MediatR;

namespace Application.Features.PostsFeatures.Posts.Queries
{
  public class GetPostByIdQuery : IRequest<OperationResult<PostResponseDto>>
  {
      public Guid PostId { get; set; }
  }  
}

