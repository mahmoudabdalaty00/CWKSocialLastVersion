using Application.Service.DTOs.PostCommentDto;
using Application.Service.Interface.Common;
using Application.Service.Interface.Services.PostServices;
using AutoMapper;
using Domain.Models.Posts;

namespace Application.Service.Implementation.Services.PostServices;

public class PostCommentService : IPostCommentService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PostCommentService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PostCommentResponseDto> GetByIdAsync(int id)
    {
        var comment = await _unitOfWork.PostCommentRepository.GetByIdAsync(id);
        if (comment == null)
            throw new KeyNotFoundException($"PostComment with ID {id} not found.");

        return _mapper.Map<PostCommentResponseDto>(comment);
    }

    public async Task<IReadOnlyList<PostCommentResponseDto>> GetByPostIdAsync(int postId)
    {
        var comments = await _unitOfWork.PostCommentRepository.GetByPostIdAsync(postId);
        return _mapper.Map<IReadOnlyList<PostCommentResponseDto>>(comments);
    }

    public async Task<IReadOnlyList<PostCommentResponseDto>> GetAllActiveByPostIdAsync(int postId)
    {
        var comments = await _unitOfWork.PostCommentRepository.GetAllActiveByPostIdAsync(postId);
        return _mapper.Map<IReadOnlyList<PostCommentResponseDto>>(comments);
    }

    public async Task<PostCommentResponseDto> CreateAsync(CreatePostCommentDto dto)
    {
        var comment = _mapper.Map<PostComment>(dto);

        await _unitOfWork.PostCommentRepository.AddAsync(comment);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<PostCommentResponseDto>(comment);
    }

    public async Task<PostCommentResponseDto> UpdateAsync(int id, UpdatePostCommentDto dto)
    {
        var comment = await _unitOfWork.PostCommentRepository.GetByIdAsync(id);
        if (comment == null)
            throw new KeyNotFoundException($"PostComment with ID {id} not found.");

        comment.Update(dto.Text);

        _unitOfWork.PostCommentRepository.Update(comment);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<PostCommentResponseDto>(comment);
    }

    public async Task DeleteAsync(int id)
    {
        var comment = await _unitOfWork.PostCommentRepository.GetByIdAsync(id);
        if (comment == null)
            throw new KeyNotFoundException($"PostComment with ID {id} not found.");

        comment.Delete();
        _unitOfWork.PostCommentRepository.Update(comment);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task RestoreAsync(int id)
    {
        var comment = await _unitOfWork.PostCommentRepository.GetByIdAsync(id);
        if (comment == null)
            throw new KeyNotFoundException($"PostComment with ID {id} not found.");

        comment.Restore();
        _unitOfWork.PostCommentRepository.Update(comment);
        await _unitOfWork.SaveChangesAsync();
    }
}
