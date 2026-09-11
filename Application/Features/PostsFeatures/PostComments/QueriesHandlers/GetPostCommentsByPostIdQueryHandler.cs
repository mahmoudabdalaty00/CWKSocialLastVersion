using Application.Features.PostsFeatures.PostComments.Queries;
using Application.Models;
using Application.Service.DTOs.PostCommentDto;
using Application.Service.Interface.Services.PostServices;
using Domain.Models.Conasts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.PostsFeatures.PostComments.QueriesHandlers
{
    public class GetPostCommentsByPostIdQueryHandler : IRequestHandler<GetPostCommentsByPostIdQuery, OperationResult<IEnumerable<PostCommentResponseDto>>>
    {
        private readonly IPostCommentService _postCommentService;

        public GetPostCommentsByPostIdQueryHandler(IPostCommentService postCommentService)
        {
            _postCommentService = postCommentService;
        }

        public async Task<OperationResult<IEnumerable<PostCommentResponseDto>>> Handle(GetPostCommentsByPostIdQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<IEnumerable<PostCommentResponseDto>>();

            try
            {
                result.Result = await _postCommentService.GetAllActiveByPostIdAsync(request.PostId);
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
