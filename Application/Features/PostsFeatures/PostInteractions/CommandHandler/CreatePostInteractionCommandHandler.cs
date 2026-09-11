using Application.Features.PostsFeatures.PostInteractions.Command;
using Application.Models;
using Application.Service.DTOs.PostInterActionDto;
using Application.Service.Interface.Services.PostServices;
using Domain.Exceptions;
using Domain.Models.Conasts;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.PostsFeatures.PostInteractions.CommandHandler
{
    public class CreatePostInteractionCommandHandler : IRequestHandler<CreatePostInteractionCommand, OperationResult<PostInterActionResponseDto>>
    {
        private readonly IPostInterActionService _postInterActionService;

        public CreatePostInteractionCommandHandler(IPostInterActionService postInterActionService)
        {
            _postInterActionService = postInterActionService;
        }

        public async Task<OperationResult<PostInterActionResponseDto>> Handle(CreatePostInteractionCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PostInterActionResponseDto>();

            try
            {
                var dto = new CreatePostInterActionDto
                {
                    PostId = request.PostId,
                    ReactionType = request.ReactionType
                };

                result.Result = await _postInterActionService.CreateAsync(dto);
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
