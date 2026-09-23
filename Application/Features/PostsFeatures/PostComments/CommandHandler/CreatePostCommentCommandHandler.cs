using Application.DTOs.PostCommentDto;
using Application.Features.PostsFeatures.PostComments.Command;
using Application.Models;
using Application.Service.Interface.Services.PostServices;
using Domain.Exceptions;
using Domain.Models.Conasts;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.PostsFeatures.PostComments.CommandHandler
{
    public class CreatePostCommentCommandHandler : IRequestHandler<CreatePostCommentCommand, OperationResult<PostCommentResponseDto>>
    {
        private readonly IPostCommentService _postCommentService;

        public CreatePostCommentCommandHandler(IPostCommentService postCommentService)
        {
            _postCommentService = postCommentService;
        }

        public async Task<OperationResult<PostCommentResponseDto>> Handle(CreatePostCommentCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PostCommentResponseDto>();

            try
            {
                var dto = new CreatePostCommentDto
                {
                    PostId = request.PostId,
                    Text = request.Text,
                    CreatedById = request.CreatedById
                };

                result.Result = await _postCommentService.CreateAsync(dto);
            }
            catch (DomainValidationException ex)
            {
                result.IsError = true;
                result.AddErrors(ex.ValidationErrors.Select(error => new Error { Code = ErrorCodes.ValidationError, Message = error }));
            }
            catch (Exception ex)
            {
                result.IsError = true;
                result.AddError(ErrorCodes.ServerError, ex.Message );
            }

            return result;
        }
    }
}
