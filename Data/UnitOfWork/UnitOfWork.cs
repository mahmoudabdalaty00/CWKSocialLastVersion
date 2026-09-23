using Data.IRepository.Base;
using Data.MainDb;
using Data.Repository.Base;
using Domain.Models.Posts;
using Domain.Models.UserProfiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Data.UnitOfWork;

public sealed class UnitOfWork : UnitOfWorkBase
{
    private readonly ReadDBContext _readCtx;

    //private readonly ICacheInterface cache;
    private readonly WriteDbContext _writeCtx;

    public UnitOfWork(WriteDbContext writeCtx, ReadDBContext readCtx) : base(writeCtx)
    {
        _writeCtx = writeCtx;
        _readCtx = readCtx;

    }





    private IRepositoryBase<UserProfile, string> _UserProfileRepository;

    public IRepositoryBase<UserProfile, string> UserProfileRepository
    {
        get
        {
            if (_UserProfileRepository == null)
                _UserProfileRepository =
                    new RepositoryBase<UserProfile, string>(writeCtx: _writeCtx, readCtx: _readCtx);
            return _UserProfileRepository;
        }
    }



    private IRepositoryBase<Post, string> _PostRepository;

    public IRepositoryBase<Post, string> PostRepository
    {
        get
        {
            if (_PostRepository == null)
                _PostRepository =
                    new RepositoryBase<Post, string>(writeCtx: _writeCtx, readCtx: _readCtx);
            return _PostRepository;
        }
    }



    private IRepositoryBase<PostComment, int> _PostCommentRepository;

    public IRepositoryBase<PostComment, int> PostCommentRepository
    {
        get
        {
            if (_PostCommentRepository == null)
                _PostCommentRepository =
                    new RepositoryBase<PostComment, int>(writeCtx: _writeCtx, readCtx: _readCtx);
            return _PostCommentRepository   ;
        }
    }

    private IRepositoryBase<PostInterAction, int> _PostInterActionRepository;

    public IRepositoryBase<PostInterAction, int> PostInterActionRepository  
    {
        get
        {
            if (_PostInterActionRepository == null)
                _PostInterActionRepository =
                    new RepositoryBase<PostInterAction, int>(writeCtx: _writeCtx, readCtx: _readCtx);
            return _PostInterActionRepository;
        }
    }





}
