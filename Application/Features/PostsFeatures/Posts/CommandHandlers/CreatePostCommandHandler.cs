using Application.Features.PostsFeatures.Posts.Commands;
using Application.Models;
using Application.Service.DTOs.PostDto;
using Application.Service.Interface.Services.PostServices;
using AutoMapper;
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
        private readonly IMapper _mapper;   
        public CreatePostCommandHandler(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;

        }

        public async Task<OperationResult<PostResponseDto>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PostResponseDto>();

            try
            {
               var dto = _mapper.Map<CreatePostDto>(request);
                result = await _postService.CreateAsync(dto);
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
