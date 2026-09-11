using Application.Service.DTOs.PostDto;
using Application.Service.Interface.Common;
using Application.Service.Interface.Services.PostServices;
using AutoMapper;
using Domain.Models.Posts;

namespace Application.Service.Implementation.Services.PostServices;

public class PostService : IPostService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PostService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
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

    public async Task<PostResponseDto> CreateAsync(CreatePostDto dto)
    {
        var post = _mapper.Map<Post>(dto);

        await _unitOfWork.PostRepository.AddAsync(post);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<PostResponseDto>(post);
    }

    public async Task<PostResponseDto> UpdateAsync(int id, UpdatePostDto dto)
    {
        var post = await _unitOfWork.PostRepository.GetByIdAsync(id);
        if (post == null)
            throw new KeyNotFoundException($"Post with ID {id} not found.");

        post.Update(dto.Content, dto.MediaUrl, dto.PostType, dto.PrivacySetting);

        _unitOfWork.PostRepository.Update(post);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<PostResponseDto>(post);
    }

    public async Task DeleteAsync(int id)
    {
        var post = await _unitOfWork.PostRepository.GetByIdAsync(id);
        if (post == null)
            throw new KeyNotFoundException($"Post with ID {id} not found.");

        post.Delete();
        _unitOfWork.PostRepository.Update(post);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task RestoreAsync(int id)
    {
        var post = await _unitOfWork.PostRepository.GetByIdAsync(id);
        if (post == null)
            throw new KeyNotFoundException($"Post with ID {id} not found.");

        post.Restore();
        _unitOfWork.PostRepository.Update(post);
        await _unitOfWork.SaveChangesAsync();
    }
}
