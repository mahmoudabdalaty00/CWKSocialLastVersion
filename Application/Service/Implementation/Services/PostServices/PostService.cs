using Application.Models;
using Application.Service.DTOs.PostDto;
using Application.Service.DTOs.UserProfileDto;
using Application.Service.Interface.Common;
using Application.Service.Interface.Services.PostServices;
using AutoMapper;
using Domain.Exceptions;
using Domain.Models.Conasts;
using Domain.Models.Posts;
using Domain.Models.UserProfiles;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Application.Service.Implementation.Services.PostServices;

public class PostService : IPostService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<CreatePostDto> _createValidator;
    private readonly IValidator<UpdatePostDto> _updateValidator;

    public PostService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreatePostDto> createValidator, IValidator<UpdatePostDto> updateValidator)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _createValidator = createValidator ?? throw new ArgumentNullException(nameof(createValidator));
        _updateValidator = updateValidator ?? throw new ArgumentNullException(nameof(updateValidator));
    }

    public async Task<PostResponseDto> GetByIdAsync(int id)
    {
        var post = await _unitOfWork.PostRepository.GetByIdAsync(id);
        if (post == null)
            throw new KeyNotFoundException($"Post with ID {id} not found.");

        return _mapper.Map<PostResponseDto>(post);
    }

    public async Task<IReadOnlyList<PostResponseDto>> GetByUserIdAsync(Guid userProfileId)
    {
        var posts = await _unitOfWork.PostRepository.GetByUserIdAsync(userProfileId);
        return _mapper.Map<IReadOnlyList<PostResponseDto>>(posts);
    }

    public async Task<IReadOnlyList<PostResponseDto>> GetAllActiveAsync()
    {
        var posts = await _unitOfWork.PostRepository.GetAllActiveAsync();
        return _mapper.Map<IReadOnlyList<PostResponseDto>>(posts);
    }





    public async Task<OperationResult<PostResponseDto>> CreateAsync(CreatePostDto dto)
    {
        var result = new OperationResult<PostResponseDto>();

        try
        {
            // Input validation
            if (dto == null)
            {
                result.AddError(ErrorCodes.ValidationError, "Post data cannot be null.");
                return result;
            }

            // FluentValidation
            var validationResult = await _createValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                result.AddErrors(validationResult.Errors.Select(e =>
                    new Error { Code = ErrorCodes.ValidationError, Message = e.ErrorMessage }));
                return result;
            }

            // Verify user exists
            var userProfile = await _unitOfWork.UserProfileRepository.GetByIdAsync(dto.UserProfileId);
            if (userProfile == null)
            {
                result.AddError(ErrorCodes.NotFound, $"User profile with ID {dto.UserProfileId} not found.");
                return result;
            }

            // Map DTO to domain model
            var post = _mapper.Map<Post>(dto);

            // Add and save
            await _unitOfWork.PostRepository.AddAsync(post);
            await _unitOfWork.SaveChangesAsync();

            result.SetSuccess(_mapper.Map<PostResponseDto>(post));
        }
        catch (DbUpdateException ex)
        {
            result.AddError(ErrorCodes.DbError, "An error occurred while saving the post to the database.");
        }
        catch (ArgumentException ex)
        {
            result.AddError(ErrorCodes.ValidationError, ex.Message);
        }
        catch (PostNotValidException ex)
        {
            result.AddErrors(ex.ValidationErrors.Select(error =>
                new Error { Code = ErrorCodes.ValidationError, Message = error }));
        }
        catch (Exception ex)
        {
            result.AddError(ErrorCodes.ServerError, "An unexpected error occurred while creating the post.");
        }

        return result;
    }

    /// <summary>
    /// Updates an existing post with validation and error handling.
    /// </summary>
    public async Task<OperationResult<PostResponseDto>> UpdateAsync(int id, UpdatePostDto dto)
    {
        var result = new OperationResult<PostResponseDto>();

        try
        {
            // Input validation
            if (id <= 0)
            {
                result.AddError(ErrorCodes.ValidationError, "Post ID must be greater than zero.");
                return result;
            }

            if (dto == null)
            {
                result.AddError(ErrorCodes.ValidationError, "Post data cannot be null.");
                return result;
            }

            // FluentValidation
            var validationResult = await _updateValidator.ValidateAsync(dto);
            if (!validationResult.IsValid)
            {
                result.AddErrors(validationResult.Errors.Select(e =>
                    new Error { Code = ErrorCodes.ValidationError, Message = e.ErrorMessage }));
                return result;
            }

            // Retrieve post
            var post = await _unitOfWork.PostRepository.GetByIdAsync(id);
            if (post == null || post.IsDeleted)
            {
                result.AddError(ErrorCodes.NotFound, $"Post with ID {id} not found.");
                return result;
            }

            // Update post
            post.Update(dto.Content, dto.MediaUrl, dto.PostType, dto.PrivacySetting);

            _unitOfWork.PostRepository.Update(post);
            await _unitOfWork.SaveChangesAsync();

            result.SetSuccess(_mapper.Map<PostResponseDto>(post));
        }
        catch (DbUpdateException ex)
        {
            result.AddError(ErrorCodes.DbError, "An error occurred while saving the post to the database.");
        }
        catch (ArgumentException ex)
        {
            result.AddError(ErrorCodes.ValidationError, ex.Message);
        }
        catch (PostNotValidException ex)
        {
            result.AddErrors(ex.ValidationErrors.Select(error =>
                new Error { Code = ErrorCodes.ValidationError, Message = error }));
        }
        catch (Exception ex)
        {
            result.AddError(ErrorCodes.ServerError, "An unexpected error occurred while updating the post.");
        }

        return result;
    }

    /// <summary>
    /// Soft deletes a post.
    /// </summary>
    public async Task<OperationResult<PostResponseDto>> DeleteAsync(int id)
    {
        var result = new OperationResult<PostResponseDto>();

        try
        {
            if (id <= 0)
            {
                result.AddError(ErrorCodes.ValidationError, "Post ID must be greater than zero.");
                return result;
            }

            var post = await _unitOfWork.PostRepository.GetByIdAsync(id);
            if (post == null || post.IsDeleted)
            {
                result.AddError(ErrorCodes.NotFound, $"Post with ID {id} not found.");
                return result;
            }

            post.Delete();
            _unitOfWork.PostRepository.Update(post);
            await _unitOfWork.SaveChangesAsync();

            result.SetSuccess(_mapper.Map<PostResponseDto>(post));
        }
        catch (DbUpdateException ex)
        {
            result.AddError(ErrorCodes.DbError, "An error occurred while deleting the post.");
        }
        catch (ArgumentException ex)
        {
            result.AddError(ErrorCodes.ValidationError, ex.Message);
        }
        catch (Exception ex)
        {
            result.AddError(ErrorCodes.ServerError, "An unexpected error occurred while deleting the post.");
        }

        return result;
    }

    /// <summary>
    /// Restores a soft-deleted post.
    /// </summary>
    public async Task<OperationResult<PostResponseDto>> RestoreAsync(int id)
    {
        var result = new OperationResult<PostResponseDto>();

        try
        {
            if (id <= 0)
            {
                result.AddError(ErrorCodes.ValidationError, "Post ID must be greater than zero.");
                return result;
            }

            var post = await _unitOfWork.PostRepository.GetByIdAsync(id);
            if (post == null)
            {
                result.AddError(ErrorCodes.NotFound, $"Post with ID {id} not found.");
                return result;
            }

            if (!post.IsDeleted)
            {
                result.AddError(ErrorCodes.ValidationError, "Post is not deleted and cannot be restored.");
                return result;
            }

            post.Restore();
            _unitOfWork.PostRepository.Update(post);
            await _unitOfWork.SaveChangesAsync();

            result.SetSuccess(_mapper.Map<PostResponseDto>(post));
        }
        catch (DbUpdateException ex)
        {
            result.AddError(ErrorCodes.DbError, "An error occurred while restoring the post.");
        }
        catch (ArgumentException ex)
        {
            result.AddError(ErrorCodes.ValidationError, ex.Message);
        }
        catch (Exception ex)
        {
            result.AddError(ErrorCodes.ServerError, "An unexpected error occurred while restoring the post.");
        }

        return result;
    }
}
