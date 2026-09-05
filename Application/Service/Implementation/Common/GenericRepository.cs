using Application.Service.Interface.Common;
using Data.MainDb;
using Microsoft.EntityFrameworkCore;

namespace Application.Service.Implementation.Common
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbSet<T> _set;

        public GenericRepository(DataContext context) => _set = context.Set<T>();

        public async Task<T?> GetByIdAsync(object id) => await _set.FindAsync(id);

        public async Task<IReadOnlyList<T>> GetAllAsync() => await _set.ToListAsync();

        public IQueryable<T> Query() => _set.AsQueryable();

        public async Task AddAsync(T entity) => await _set.AddAsync(entity);

        public void Update(T entity) => _set.Update(entity);

        public void Remove(T entity) => _set.Remove(entity);
    }



}
