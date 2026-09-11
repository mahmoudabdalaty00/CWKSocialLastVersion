using Application.Features.PostsFeatures.Posts.Queries;
using Application.Models;
using Application.Service.DTOs.PostDto;
using Application.Service.Interface.Services.PostServices;
using Domain.Models.Conasts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.PostsFeatures.Posts.QueryHandlers
{
    public class GetAllPostsQueryHandler : IRequestHandler<GetAllPostsQuery, OperationResult<IEnumerable<PostResponseDto>>>
    {
        private readonly IPostService _postService;

        public GetAllPostsQueryHandler(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<OperationResult<IEnumerable<PostResponseDto>>> Handle(GetAllPostsQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<IEnumerable<PostResponseDto>>();

            try
            {
                result.Result = await _postService.GetAllActiveAsync();
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.Errors.Add(new Error { Code = ErrorCodes.ServerError, Message = ex.Message });
            }

            return result;
        }
    }
}
