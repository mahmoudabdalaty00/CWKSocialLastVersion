using Application.Features.PostsFeatures.PostComments.Command;
using Application.Models;
using Application.Service.DTOs.PostCommentDto;
using Application.Service.Interface.Services.PostServices;
using Domain.Models.Conasts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.PostsFeatures.PostComments.CommandHandler
{
    public class DeletePostCommentCommandHandler : IRequestHandler<DeletePostCommentCommand, OperationResult<PostCommentResponseDto>>
    {
        private readonly IPostCommentService _postCommentService;

        public DeletePostCommentCommandHandler(IPostCommentService postCommentService)
        {
            _postCommentService = postCommentService;
        }

        public async Task<OperationResult<PostCommentResponseDto>> Handle(DeletePostCommentCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PostCommentResponseDto>();

            try
            {
                await _postCommentService.DeleteAsync(request.Id);
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
