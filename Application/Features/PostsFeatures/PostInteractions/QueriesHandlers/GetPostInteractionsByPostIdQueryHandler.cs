using Application.Features.PostsFeatures.PostInteractions.Queries;
using Application.Models;
using Application.Service.DTOs.PostInterActionDto;
using Application.Service.Interface.Services.PostServices;
using Domain.Models.Conasts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.PostsFeatures.PostInteractions.QueriesHandlers
{
    public class GetPostInteractionsByPostIdQueryHandler : IRequestHandler<GetPostInteractionsByPostIdQuery, OperationResult<IEnumerable<PostInterActionResponseDto>>>
    {
        private readonly IPostInterActionService _postInterActionService;

        public GetPostInteractionsByPostIdQueryHandler(IPostInterActionService postInterActionService)
        {
            _postInterActionService = postInterActionService;
        }

        public async Task<OperationResult<IEnumerable<PostInterActionResponseDto>>> Handle(GetPostInteractionsByPostIdQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<IEnumerable<PostInterActionResponseDto>>();

            try
            {
                result.Result = await _postInterActionService.GetAllActiveByPostIdAsync(request.PostId);
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
