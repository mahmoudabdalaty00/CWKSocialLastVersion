using Application.DTOs.PostDto;
using Application.Models;
using MediatR;

namespace Application.Features.PostsFeatures.Posts.Queries
{
  public class GetPostByIdQuery : IRequest<OperationResult<PostResponseDto>>
  {
      public string PostId { get; set; }
  }  
}

