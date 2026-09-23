using Application.DTOs.PostInterActionDto;
using Application.Service.Interface.Base;
using Application.Service.Interface.Services.PostServices;
using AutoMapper;
using Data.UnitOfWork;
using Domain.Models.Posts;

namespace Application.Service.Implementation.PostServices;

public class PostInterActionService : IPostInterActionService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public event ServiceLoggerEventHandler ServiceLogger;

    public PostInterActionService(UnitOfWork unitOfWork, IMapper mapper)
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

    public async Task<IReadOnlyList<PostInterActionResponseDto>> GetByPostIdAsync(string postId)
    {
        var interactions = await _unitOfWork.PostInterActionRepository.GetAsync(e => e.PostId == postId , orderBy:e=> e.OrderBy(s =>s.CreatedAt));
        return _mapper.Map<IReadOnlyList<PostInterActionResponseDto>>(interactions);
    }

    //public async Task<IReadOnlyList<PostInterActionResponseDto>> GetAllActiveByPostIdAsync(string postId)
    //{
    //    var interactions = await _unitOfWork.PostInterActionRepository.GetAllActiveByPostIdAsync(postId);
    //    return _mapper.Map<IReadOnlyList<PostInterActionResponseDto>>(interactions);
    //}

    public async Task<PostInterActionResponseDto> CreateAsync(CreatePostInterActionDto dto)
    {
        var interaction = _mapper.Map<PostInterAction>(dto);

        await _unitOfWork.PostInterActionRepository.AddAsync(interaction);
        await _unitOfWork.SaveChangesAsync(false);

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
       
        await _unitOfWork.SaveChangesAsync(false);
        return _mapper.Map<PostInterActionResponseDto>(interaction);
    }

    public async Task DeleteAsync(int id)
    {
        var interaction = await _unitOfWork.PostInterActionRepository.GetByIdAsync(id);
        if (interaction == null)
            throw new KeyNotFoundException($"PostInterAction with ID {id} not found.");

        interaction.Delete();
        _unitOfWork.PostInterActionRepository.Update(interaction);
        await _unitOfWork.SaveChangesAsync(false);
    }

    public async Task RestoreAsync(int id)
    {
        var interaction = await _unitOfWork.PostInterActionRepository.GetByIdAsync(id);
        if (interaction == null)
            throw new KeyNotFoundException($"PostInterAction with ID {id} not found.");

        interaction.Restore();
        _unitOfWork.PostInterActionRepository.Update(interaction);
        await _unitOfWork.SaveChangesAsync(false);
    }
}
