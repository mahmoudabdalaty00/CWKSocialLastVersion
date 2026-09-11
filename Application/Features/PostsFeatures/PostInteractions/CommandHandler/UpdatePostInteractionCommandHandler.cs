using Application.Features.PostsFeatures.PostInteractions.Command;
using Application.Models;
using Application.Service.DTOs.PostInterActionDto;
using Application.Service.Interface.Services.PostServices;
using Domain.Exceptions;
using Domain.Models.Conasts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.PostsFeatures.PostInteractions.CommandHandler
{
    public class UpdatePostInteractionCommandHandler : IRequestHandler<UpdatePostInteractionCommand, OperationResult<PostInterActionResponseDto>>
    {
        private readonly IPostInterActionService _postInterActionService;

        public UpdatePostInteractionCommandHandler(IPostInterActionService postInterActionService)
        {
            _postInterActionService = postInterActionService;
        }

        public async Task<OperationResult<PostInterActionResponseDto>> Handle(UpdatePostInteractionCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PostInterActionResponseDto>();

            try
            {
                var dto = new UpdatePostInterActionDto
                {
                    ReactionType = request.ReactionType
                };

                result.Result = await _postInterActionService.UpdateAsync(request.Id, dto);
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
