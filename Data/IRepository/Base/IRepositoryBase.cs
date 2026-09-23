using AutoMapper;
using Data.Specifications.Abstraction;
using Domain.Models.BaseEntities;
using JqueryDataTables.ServerSide.AspNetCoreWeb.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Data.IRepository.Base
{
    public interface IRepositoryBase<TEntity, TId>
        //where TId : struct
        where TEntity : BaseEntity<TId>
    {
        public EntityEntry<TEntity> Add(TEntity entity);

        public Task<EntityEntry<TEntity>> AddAsync(TEntity entity);

        public void AddRange(IEnumerable<TEntity> entity);
        public void RemoveRange(IEnumerable<TEntity> entity);

        public Task AddRangeAsync(IEnumerable<TEntity> entity);

        public Task<bool> Any(Expression<Func<TEntity, bool>> Predicate);

        public Task DeleteAsync(TId id);

        public void Delete(TEntity entity);
        public void DeleteRange(IEnumerable<TEntity> entities);

        public Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> Predicate, bool? isTracked = false);
        public Task<IEnumerable<TEntity>> GetAll(bool? isTracked = false);

        public Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, bool? isTracked = false);

        public Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate,
            string? includeString = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            bool? isTracked = false);

        public Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate,
            List<Expression<Func<TEntity, object>>> includesList,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, bool? isTracked = false);

        public Task<TEntity?> GetByIdAsync(Expression<Func<TEntity, bool>>? predicate,
            List<Expression<Func<TEntity, object>>>? includesList,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, bool? isTracked = false);

        IQueryable<TEntity> Include(Expression<Func<TEntity, object>> include);

        public Task<IEnumerable<R>> GetProjectionAsync<R>(IConfigurationProvider config,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            List<Expression<Func<TEntity, object>>>? includesList = null, bool? isTracked = false,
            object? ProjectionParameters = null);

        public Task<R> GetFirstProjectionAsync<R>(IConfigurationProvider config,
            Expression<Func<TEntity, bool>>? predicate = null, object? ProjectionParameters = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            List<Expression<Func<TEntity, object>>>? includesList = null, bool? isTracked = false);

        public Task<TEntity> GetByIdAsync(TId Id);
        public Task<bool> SaveChanges();
        public void Update(TEntity entity, params Expression<Func<TEntity, object?>>?[]? properties);
        void UpdateRange(IEnumerable<TEntity> entities);
        public Task<TEntity> GetFirstSpecificationAsync(BaseSpecifications<TEntity> specifications);

        public Task<IEnumerable<TEntity>> GetSpecificationAsync(BaseSpecifications<TEntity> specifications);

        Task<IEnumerable<R>> GetSpecificationProjectionAsync<R>(BaseSpecifications<TEntity> specifications,
            IConfigurationProvider config);

        Task<IEnumerable<R>> GetSpecificationProjectionAsync<R>(BaseSpecifications<TEntity> specifications,
            IConfigurationProvider config, object projectionParameters);

        public IQueryable<TEntity> GetQuery();
        public IQueryable<TEntity> GetQueryFromWrite();

        Task<JqueryDataTablesPagedResults<TReturn>> GetDataTableProjectionAsync<TReturn>(
            Func<IQueryable<TEntity>, IQueryable<TEntity>> query, JqueryDataTablesParameters table,
            IConfigurationProvider config, bool ShowAllData) where TReturn : class;

        public Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null);
    }
}