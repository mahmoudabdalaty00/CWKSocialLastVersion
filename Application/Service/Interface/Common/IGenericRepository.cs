using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Service.Interface.Common
{
    /// <summary>
    /// Generic CRUD surface over a single entity type. For anything beyond
    /// basic CRUD (filtering, Include, paging, projections), use Query().
    /// </summary>
    public interface IGenericRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(object id);
        Task<IReadOnlyList<T>> GetAllAsync();
        IQueryable<T> Query();
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
    }

}
