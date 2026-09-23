using Application.DTOs.PostDto;
using Application.Models;
using MediatR;

namespace Application.Features.PostsFeatures.Posts.Queries;

public class GetAllPostsQuery : IRequest<OperationResult<IEnumerable<PostResponseDto>>>
{
}