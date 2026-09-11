using Application.Service.DTOs.PostInterActionDto;
using Application.Service.Interface.Common;
using Application.Service.Interface.Services.PostServices;
using AutoMapper;
using Domain.Models.Posts;

namespace Application.Service.Implementation.Services.PostServices;

public class PostInterActionService : IPostInterActionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PostInterActionService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PostInterActionResponseDto> GetByIdAsync(int id)
    {
        var interaction = await _unitOfWork.PostInterActionRepository.GetByIdAsync(id);
        if (interaction == null)
            throw new KeyNotFoundException($"PostInterAction with ID {id} not found.");

        return _mapper.Map<PostInterActionResponseDto>(interaction);
    }

    public async Task<IReadOnlyList<PostInterActionResponseDto>> GetByPostIdAsync(int postId)
    {
        var interactions = await _unitOfWork.PostInterActionRepository.GetByPostIdAsync(postId);
        return _mapper.Map<IReadOnlyList<PostInterActionResponseDto>>(interactions);
    }

    public async Task<IReadOnlyList<PostInterActionResponseDto>> GetAllActiveByPostIdAsync(int postId)
    {
        var interactions = await _unitOfWork.PostInterActionRepository.GetAllActiveByPostIdAsync(postId);
        return _mapper.Map<IReadOnlyList<PostInterActionResponseDto>>(interactions);
    }

    public async Task<PostInterActionResponseDto> CreateAsync(CreatePostInterActionDto dto)
    {
        var interaction = _mapper.Map<PostInterAction>(dto);

        await _unitOfWork.PostInterActionRepository.AddAsync(interaction);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<PostInterActionResponseDto>(interaction);
    }

    public async Task<PostInterActionResponseDto> UpdateAsync(int id, UpdatePostInterActionDto dto)
    {
        var interaction = await _unitOfWork.PostInterActionRepository.GetByIdAsync(id);
        if (interaction == null)
            throw new KeyNotFoundException($"PostInterAction with ID {id} not found.");

        try
        {
            interaction.Update(dto.ReactionType);
            _unitOfWork.PostInterActionRepository.Update(interaction);

        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to update PostInterAction with ID {id}.", ex);
        }
       
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<PostInterActionResponseDto>(interaction);
    }

    public async Task DeleteAsync(int id)
    {
        var interaction = await _unitOfWork.PostInterActionRepository.GetByIdAsync(id);
        if (interaction == null)
            throw new KeyNotFoundException($"PostInterAction with ID {id} not found.");

        interaction.Delete();
        _unitOfWork.PostInterActionRepository.Update(interaction);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task RestoreAsync(int id)
    {
        var interaction = await _unitOfWork.PostInterActionRepository.GetByIdAsync(id);
        if (interaction == null)
            throw new KeyNotFoundException($"PostInterAction with ID {id} not found.");

        interaction.Restore();
        _unitOfWork.PostInterActionRepository.Update(interaction);
        await _unitOfWork.SaveChangesAsync();
    }
}
