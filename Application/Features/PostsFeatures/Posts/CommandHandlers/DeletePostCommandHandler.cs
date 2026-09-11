using Application.Features.PostsFeatures.Posts.Commands;
using Application.Models;
using Application.Service.DTOs.PostDto;
using Application.Service.Interface.Services.PostServices;
using Domain.Models.Conasts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.PostsFeatures.Posts.CommandHandlers
{
    public class DeletePostCommandHandler : IRequestHandler<DeletePostCommand, OperationResult<PostResponseDto>>
    {
        private readonly IPostService _postService;

        public DeletePostCommandHandler(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<OperationResult<PostResponseDto>> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PostResponseDto>();

            try
            {
                await _postService.DeleteAsync(request.Id);
            }
            catch (KeyNotFoundException ex)
            {
                result.IsError = true;
                result.Errors.Add(new Error { Code = ErrorCodes.NotFound, Message = ex.Message });
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
