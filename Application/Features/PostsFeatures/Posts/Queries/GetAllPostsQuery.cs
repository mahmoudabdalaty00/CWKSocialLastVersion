using Application.Models;
using Application.Service.DTOs.PostDto;
using MediatR;

namespace Application.Features.PostsFeatures.Posts.Queries;

public class GetAllPostsQuery : IRequest<OperationResult<IEnumerable<PostResponseDto>>>
{
}