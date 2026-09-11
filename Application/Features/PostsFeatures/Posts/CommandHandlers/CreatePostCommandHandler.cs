using Application.Features.PostsFeatures.Posts.Commands;
using Application.Models;
using Application.Service.DTOs.PostDto;
using Application.Service.Interface.Services.PostServices;
using Domain.Exceptions;
using Domain.Models.Conasts;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.PostsFeatures.Posts.CommandHandlers
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand, OperationResult<PostResponseDto>>
    {
        private readonly IPostService _postService;

        public CreatePostCommandHandler(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<OperationResult<PostResponseDto>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PostResponseDto>();

            try
            {
                var dto = new CreatePostDto
                {
                    Content = request.Content,
                    MediaUrl = request.MediaUrl,
                    PostType = request.PostType,
                    PrivacySetting = request.PrivacySetting,
                    UserProfileId = request.UserProfileId
                };

                result.Result = await _postService.CreateAsync(dto);
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
