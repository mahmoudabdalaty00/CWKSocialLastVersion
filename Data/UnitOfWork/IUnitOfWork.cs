using Microsoft.EntityFrameworkCore.Storage;

namespace Data.UnitOfWork;
 
public interface IUnitOfWork
{
    IDbContextTransaction BeginTransaction();
    int SaveChanges(bool isTransaction);
    Task<int> SaveChangesAsync(bool isTransaction);
}


