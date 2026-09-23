using Data.MainDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Data.UnitOfWork;

public abstract class UnitOfWorkBase : IUnitOfWork, IDisposable

{
    protected WriteDbContext _writeCtx;
    public UnitOfWorkBase(WriteDbContext writeCtx)
    {
        _writeCtx = writeCtx;
    }
    public IDbContextTransaction BeginTransaction()
    {
        return _writeCtx.Database.BeginTransaction();
    }

    public int SaveChanges(bool isTransaction)
    {
        //save changes was called on a non transaction query
        int state = -1;
        if (!isTransaction)
        {

            return SaveChanges();
        }
        else
        {
            try
            {
                _writeCtx.Database.CommitTransaction();
                return 1;
            }
            catch
            {
                _writeCtx.Database.RollbackTransaction();
                return state;
            }
        }
    }

    public async Task<int> SaveChangesAsync(bool isTransaction)
    {
        //save changes was called on a non transaction query
        int state = -1;
        if (!isTransaction)
        {
            return await SaveChangesAsync();
        }
        else
        {
            try
            {
                await _writeCtx.Database.CommitTransactionAsync();
                return 1;
            }
            catch
            {
                await _writeCtx.Database.RollbackTransactionAsync();
                return state;
            }
        }
    }

    private int SaveChanges()
    {
        int state = -1;
        try
        {
            return _writeCtx.SaveChanges();
        }
        catch
        {
            return state;
        }
    }

    public void ForceDetach(object entry)
    {
        _writeCtx.Entry(entry).State = EntityState.Detached;
    }

    private async Task<int> SaveChangesAsync()
    {
        int state = -1;
        try
        {
            return await _writeCtx.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return state;
        }
    }

    private bool isDisposed { get; set; } = false;
    public void Dispose()
    {
        if (isDisposed == false)
        {
            isDisposed = true;
            //_context.Dispose();
        }
        GC.SuppressFinalize(this);
    }
}