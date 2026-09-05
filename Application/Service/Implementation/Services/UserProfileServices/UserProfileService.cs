using Application.Service.DTOs.UserProfileDto;
using Application.Service.Interface.Common;
using AutoMapper;
using Domain.Models.UserProfiles;

namespace Application.Service.Implementation.Services.UserProfileServices;

public class UserProfileService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserProfileService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<UserProfileResponseDto> GetByIdAsync(Guid id)
    {
        var userProfile = await _unitOfWork.UserProfileRepository.GetByIdAsync(id);
        if (userProfile == null)
            throw new KeyNotFoundException($"UserProfile with ID {id} not found.");

        return _mapper.Map<UserProfileResponseDto>(userProfile);
    }

    public async Task<UserProfileResponseDto> GetByIdentityUserIdAsync(string identityUserId)
    {
        var userProfile = await _unitOfWork.UserProfileRepository.GetByIdentityUserIdAsync(identityUserId);
        if (userProfile == null)
            throw new KeyNotFoundException($"UserProfile with IdentityUserId {identityUserId} not found.");

        return _mapper.Map<UserProfileResponseDto>(userProfile);
    }

    public async Task<IEnumerable<UserProfileResponseDto>> GetAllActiveAsync()
    {
        var userProfiles = await _unitOfWork.UserProfileRepository.GetAllActiveAsync();
        return _mapper.Map<IEnumerable<UserProfileResponseDto>>(userProfiles);
    }

    public async Task<UserProfileResponseDto> CreateAsync(CreateUserProfileDto dto)
    {
        var userProfile = _mapper.Map<UserProfile>(dto);

        await _unitOfWork.UserProfileRepository.AddAsync(userProfile);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<UserProfileResponseDto>(userProfile);
    }

    public async Task<UserProfileResponseDto> UpdateAsync(Guid id, UpdateUserProfileDto dto)
    {
        var userProfile = await _unitOfWork.UserProfileRepository.GetByIdAsync(id);
        if (userProfile == null)
            throw new KeyNotFoundException($"UserProfile with ID {id} not found.");

        var updatedBasicInfo = _mapper.Map<BasicInfo>(dto);
        userProfile.UpdateBasicInfo(updatedBasicInfo);

        _unitOfWork.UserProfileRepository.Update(userProfile);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<UserProfileResponseDto>(userProfile);
    }

    public async Task DeleteAsync(Guid id)
    {
        var userProfile = await _unitOfWork.UserProfileRepository.GetByIdAsync(id);
        if (userProfile == null)
            throw new KeyNotFoundException($"UserProfile with ID {id} not found.");

        userProfile.Delete();
        _unitOfWork.UserProfileRepository.Update(userProfile);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task RestoreAsync(Guid id)
    {
        var userProfile = await _unitOfWork.UserProfileRepository.GetByIdAsync(id);
        if (userProfile == null)
            throw new KeyNotFoundException($"UserProfile with ID {id} not found.");

        userProfile.Restore();
        _unitOfWork.UserProfileRepository.Update(userProfile);
        await _unitOfWork.SaveChangesAsync();
    }
}

 