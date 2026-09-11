using Application.Features.PostsFeatures.PostInteractions.Command;
using Application.Models;
using Application.Service.DTOs.PostInterActionDto;
using Application.Service.Interface.Services.PostServices;
using Domain.Models.Conasts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.PostsFeatures.PostInteractions.CommandHandler
{
    public class DeletePostInteractionCommandHandler : IRequestHandler<DeletePostInteractionCommand, OperationResult<PostInterActionResponseDto>>
    {
        private readonly IPostInterActionService _postInterActionService;

        public DeletePostInteractionCommandHandler(IPostInterActionService postInterActionService)
        {
            _postInterActionService = postInterActionService;
        }

        public async Task<OperationResult<PostInterActionResponseDto>> Handle(DeletePostInteractionCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PostInterActionResponseDto>();

            try
            {
                await _postInterActionService.DeleteAsync(request.Id);
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
