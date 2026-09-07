using Application.Models;
using Application.Service.DTOs.UserProfileDto;
using Application.Service.Interface.Common;
using Application.Service.Interface.Services.UserProfileServices;
using AutoMapper;
using Domain.Models.Conasts;
using Domain.Models.UserProfiles;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Service.Implementation.Services.UserProfileServices;

public class UserProfileService : IUserProfileService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateUserProfileDto> _createValidator;
    private readonly IValidator<UpdateUserProfileDto> _updateValidator;

    public UserProfileService(
        IUnitOfWork unitOfWork, 
        IMapper mapper,
        IValidator<CreateUserProfileDto> createValidator,
        IValidator<UpdateUserProfileDto> updateValidator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _createValidator = createValidator;
        _updateValidator = updateValidator;
    }

    public async Task<OperationResult<UserProfileResponseDto>> GetByIdAsync(Guid id)
    {
        var result = new OperationResult<UserProfileResponseDto>();
        var userProfile = await _unitOfWork.UserProfileRepository.GetByIdAsync(id);
        
        if (userProfile == null || userProfile.IsDeleted)
        {
            result.IsError = true;
            result.Errors.Add(new Error { Code = ErrorCodes.NotFound, Message = $"User profile not found with ID : {id}." });
            return result;
        }

        result.Result = _mapper.Map<UserProfileResponseDto>(userProfile);
        return result;
    }

    public async Task<OperationResult<UserProfileResponseDto>> GetByIdentityUserIdAsync(string identityUserId)
    {
        var result = new OperationResult<UserProfileResponseDto>();
        var userProfile = await _unitOfWork.UserProfileRepository.GetByIdentityUserIdAsync(identityUserId);
        
        if (userProfile == null || userProfile.IsDeleted)
        {
            result.IsError = true;
            result.Errors.Add(new Error { Code = ErrorCodes.NotFound, Message = $"User profile not found with IdentityUserId : {identityUserId}." });
            return result;
        }

        result.Result = _mapper.Map<UserProfileResponseDto>(userProfile);
        return result;
    }

    public async Task<OperationResult<IEnumerable<UserProfileResponseDto>>> GetAllAsync()
    {
        var result = new OperationResult<IEnumerable<UserProfileResponseDto>>();
        var userProfiles = await _unitOfWork.UserProfileRepository.GetAllActiveAsync();
        result.Result = _mapper.Map<IEnumerable<UserProfileResponseDto>>(userProfiles);
        return result;
    }

    public async Task<OperationResult<UserProfileResponseDto>> CreateAsync(CreateUserProfileDto dto)
    {
        var result = new OperationResult<UserProfileResponseDto>();

        var validationResult = await _createValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            result.IsError = true;
            result.Errors.AddRange(validationResult.Errors.Select(e => new Error { Code = ErrorCodes.ValidationError, Message = e.ErrorMessage }));
            return result;
        }

        try
        {
            var basicInfo = BasicInfo.Create(dto.FirstName, dto.LastName, dto.DateOfBirth, dto.Bio, dto.Phone, dto.EmailAddress, dto.CurrentCity);
            
            var userProfile = UserProfile.Create(string.IsNullOrEmpty(dto.IdentityUserId) ? Guid.NewGuid().ToString() : dto.IdentityUserId, basicInfo);

            await _unitOfWork.UserProfileRepository.AddAsync(userProfile);
            await _unitOfWork.SaveChangesAsync();

            result.Result = _mapper.Map<UserProfileResponseDto>(userProfile);
        }
        catch (DbUpdateException ex)
        {
            result.IsError = true;
            result.Errors.Add(new Error { Code = ErrorCodes.DbError, Message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            result.IsError = true;
            result.Errors.Add(new Error { Code = ErrorCodes.ValidationError, Message = ex.Message });
        }
        catch (UserProfileNotValideException ex)
        {
            result.IsError = true;
            result.Errors.AddRange(ex.ValidationErrors.Select(error => new Error { Code = ErrorCodes.ValidationError, Message = error }));
        }

        return result;
    }

    public async Task<OperationResult<UserProfileResponseDto>> UpdateAsync(Guid id, UpdateUserProfileDto dto)
    {
        var result = new OperationResult<UserProfileResponseDto>();

        var validationResult = await _updateValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            result.IsError = true;
            result.Errors.AddRange(validationResult.Errors.Select(e => new Error { Code = ErrorCodes.ValidationError, Message = e.ErrorMessage }));
            return result;
        }

        var existsEmailForOther = await _unitOfWork.UserProfileRepository.Query()
            .AnyAsync(u => u.BasicInfo.EmailAddress.ToLower() == dto.EmailAddress.ToLower() && u.Id != id);
            
        if (existsEmailForOther)
        {
            result.IsError = true;
            result.Errors.Add(new Error { Code = ErrorCodes.ValidationError, Message = "This email address is already in use by another user." });
            return result;
        }

        try
        {
            var userProfile = await _unitOfWork.UserProfileRepository.GetByIdAsync(id);
            if (userProfile == null || userProfile.IsDeleted)
            {
                result.IsError = true;
                result.Errors.Add(new Error { Code = ErrorCodes.NotFound, Message = $"User profile not found with ID : {id}." });
                return result;
            }

            var basicInfo = BasicInfo.Create(dto.FirstName, dto.LastName, dto.DateOfBirth, dto.Bio, dto.Phone, dto.EmailAddress, dto.CurrentCity);
            userProfile.UpdateBasicInfo(basicInfo);
            userProfile.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.UserProfileRepository.Update(userProfile);
            await _unitOfWork.SaveChangesAsync();

            result.Result = _mapper.Map<UserProfileResponseDto>(userProfile);
        }
        catch (DbUpdateException ex)
        {
            result.IsError = true;
            result.Errors.Add(new Error { Code = ErrorCodes.DbError, Message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            result.IsError = true;
            result.Errors.Add(new Error { Code = ErrorCodes.ValidationError, Message = ex.Message });
        }
        catch (UserProfileNotValideException ex)
        {
            result.IsError = true;
            result.Errors.AddRange(ex.ValidationErrors.Select(error => new Error { Code = ErrorCodes.ValidationError, Message = error }));
        }

        return result;
    }

    public async Task<OperationResult<UserProfileResponseDto>> DeleteAsync(Guid id)
    {
        var result = new OperationResult<UserProfileResponseDto>();
        var userProfile = await _unitOfWork.UserProfileRepository.GetByIdAsync(id);
        
        if (userProfile == null || userProfile.IsDeleted)
        {
            result.IsError = true;
            result.Errors.Add(new Error { Code = ErrorCodes.NotFound, Message = $"User profile not found with ID : {id}." });
            return result;
        }

        userProfile.IsDeleted = true;
        userProfile.DeletedAt = DateTime.UtcNow;

        _unitOfWork.UserProfileRepository.Update(userProfile);
        await _unitOfWork.SaveChangesAsync();

        result.Result = _mapper.Map<UserProfileResponseDto>(userProfile);
        return result;
    }

    public async Task<OperationResult<UserProfileResponseDto>> RestoreAsync(Guid id)
    {
        var result = new OperationResult<UserProfileResponseDto>();
        var userProfile = await _unitOfWork.UserProfileRepository.GetByIdAsync(id);
        
        if (userProfile == null)
        {
            result.IsError = true;
            result.Errors.Add(new Error { Code = ErrorCodes.NotFound, Message = $"User profile not found with ID : {id}." });
            return result;
        }

        userProfile.Restore();
        _unitOfWork.UserProfileRepository.Update(userProfile);
        await _unitOfWork.SaveChangesAsync();

        result.Result = _mapper.Map<UserProfileResponseDto>(userProfile);
        return result;
    }
}