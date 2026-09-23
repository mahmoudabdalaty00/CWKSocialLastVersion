using Application.DTOs.PostCommentDto;
using Application.Service.Interface.Services.PostServices;
using AutoMapper;
using Data.UnitOfWork;
using Domain.Models.Posts;

namespace Application.Service.Implementation.PostServices;

public class PostCommentService : IPostCommentService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public PostCommentService(UnitOfWork unitOfWork, IMapper mapper)
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

    public async Task<IReadOnlyList<PostCommentResponseDto>> GetByPostIdAsync(string postId)
    {
        var comments = await _unitOfWork.PostCommentRepository.GetAsync(e =>e.PostId == postId, orderBy :x=>x.OrderBy(s => s.CreatedAt)  );
        return _mapper.Map<IReadOnlyList<PostCommentResponseDto>>(comments);
    }

    //public async Task<IReadOnlyList<PostCommentResponseDto>> GetAllActiveByPostIdAsync(string postId)
    //{
    //    var comments = await _unitOfWork.PostCommentRepository.GetAll(e => e.PostId == postId);
    //    return _mapper.Map<IReadOnlyList<PostCommentResponseDto>>(comments);
    //}

    public async Task<PostCommentResponseDto> CreateAsync(CreatePostCommentDto dto)
    {
        var comment = _mapper.Map<PostComment>(dto);

        await _unitOfWork.PostCommentRepository.AddAsync(comment);
        await _unitOfWork.SaveChangesAsync(false);

        return _mapper.Map<PostCommentResponseDto>(comment);
    }

    public async Task<PostCommentResponseDto> UpdateAsync(int id, UpdatePostCommentDto dto)
    {
        var comment = await _unitOfWork.PostCommentRepository.GetByIdAsync(id);
        if (comment == null)
            throw new KeyNotFoundException($"PostComment with ID {id} not found.");

        comment.Update(dto.Text,dto.UpdatedById);

        _unitOfWork.PostCommentRepository.Update(comment);
        await _unitOfWork.SaveChangesAsync(false);

        return _mapper.Map<PostCommentResponseDto>(comment);
    }

    public async Task DeleteAsync(int id)
    {
        var comment = await _unitOfWork.PostCommentRepository.GetByIdAsync(id);
        if (comment == null)
            throw new KeyNotFoundException($"PostComment with ID {id} not found.");

        comment.Delete();
        _unitOfWork.PostCommentRepository.Update(comment);
        await _unitOfWork.SaveChangesAsync(false);
    }

    public async Task RestoreAsync(int id)
    {
        var comment = await _unitOfWork.PostCommentRepository.GetByIdAsync(id);
        if (comment == null)
            throw new KeyNotFoundException($"PostComment with ID {id} not found.");

        comment.Restore();
        _unitOfWork.PostCommentRepository.Update(comment);
        await _unitOfWork.SaveChangesAsync(false);
    }
}
