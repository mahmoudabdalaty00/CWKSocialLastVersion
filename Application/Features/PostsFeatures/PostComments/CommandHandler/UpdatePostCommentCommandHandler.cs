using Application.Features.PostsFeatures.PostComments.Command;
using Application.Models;
using Application.Service.DTOs.PostCommentDto;
using Application.Service.Interface.Services.PostServices;
using Domain.Exceptions;
using Domain.Models.Conasts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.PostsFeatures.PostComments.CommandHandler
{
    public class UpdatePostCommentCommandHandler : IRequestHandler<UpdatePostCommentCommand, OperationResult<PostCommentResponseDto>>
    {
        private readonly IPostCommentService _postCommentService;

        public UpdatePostCommentCommandHandler(IPostCommentService postCommentService)
        {
            _postCommentService = postCommentService;
        }

        public async Task<OperationResult<PostCommentResponseDto>> Handle(UpdatePostCommentCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PostCommentResponseDto>();

            try
            {
                var dto = new UpdatePostCommentDto
                {
                    Text = request.Text
                };

                result.Result = await _postCommentService.UpdateAsync(request.Id, dto);
            }
            catch (KeyNotFoundException ex)
            {
                result.IsError = true;
                result.Errors.Add(new Error { Code = ErrorCodes.NotFound, Message = ex.Message });
            }
            catch (DomainValidationException ex)
            {
                result.IsError = true;
                result.Errors.AddRange(ex.ValidationErrors.Select(error => new Error { Code = ErrorCodes.ValidationError, Message = error }));
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
