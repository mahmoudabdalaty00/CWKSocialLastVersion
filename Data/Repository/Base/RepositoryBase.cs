using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.IRepository.Base;
using Data.Service;
using Data.Specifications.Abstraction;
using Data.Specifications.Extention;
using Domain.Models.BaseEntities;
using JqueryDataTables.ServerSide.AspNetCoreWeb.Infrastructure;
using JqueryDataTables.ServerSide.AspNetCoreWeb.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace Data.Repository.Base
{
    public class RepositoryBase<TEntity, TId> : IRepositoryBase<TEntity, TId>
        where TEntity : BaseEntity<TId>
        //where TId : struct
    {
        protected DbContext _writeCtx;
        protected DbSet<TEntity> _writeSet;
        protected DbSet<TEntity> _readSet;

        public RepositoryBase(DbContext writeCtx, DbContext readCtx)
        {
            _writeCtx = writeCtx;
            _writeSet = writeCtx.Set<TEntity>();
            _readSet = readCtx.Set<TEntity>();
        }

        public virtual EntityEntry<TEntity> Add(TEntity entity)
        {
            return _writeSet.Add(entity);
        }

        public async Task<EntityEntry<TEntity>> AddAsync(TEntity entity)
        {
            return await _writeSet.AddAsync(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entity)
        {
            _writeSet.AddRange(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entity)
        {
            _writeSet.RemoveRange(entity);
        }

        public virtual async Task AddRangeAsync(IEnumerable<TEntity> entity)
        {
            await _writeSet.AddRangeAsync(entity);
        }


        public virtual async Task DeleteAsync(TId id)
        {
            var entity = await _writeSet.FindAsync(id);
            _writeSet.Remove(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            if (_writeCtx.Entry(entity).State == EntityState.Detached)
                _writeCtx.Attach(entity);
            _writeCtx.Remove(entity);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAll(bool? isTracked = false)
        {
            return await _readSet.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> Predicate,
            bool? isTracked = false)
        {
            var query = _readSet.Where(Predicate);
            if (isTracked == false)
                query = query.AsNoTracking();
            return await query.ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate,
            string? includeString = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            bool? isTracked = false)
        {
            IQueryable<TEntity> query = _readSet;

            if (isTracked == false)
                query = query.AsNoTracking();

            if (predicate != null)
                query = query.Where(predicate);

            if (!string.IsNullOrWhiteSpace(includeString))
            {
                var commaString = splitCommaString(includeString);
                commaString.ForEach(e => query = query.Include(e));
            }

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }


        public virtual async Task<TEntity?> GetByIdAsync(Expression<Func<TEntity, bool>>? predicate,
            string? includeString = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            bool? isTracked = false)
        {
            IQueryable<TEntity> query = _readSet;

            if (isTracked == false)
                query = query.AsNoTracking();

            if (predicate != null)
                query = query.Where(predicate);

            if (!string.IsNullOrWhiteSpace(includeString))
            {
                var commaString = splitCommaString(includeString);
                commaString.ForEach(e => query.Include(e));
            }

            if (orderBy != null)
                return await orderBy(query).FirstOrDefaultAsync();
            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? predicate = null,
            List<Expression<Func<TEntity, object>>>? includesList = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, bool? isTracked = false)
        {
            IQueryable<TEntity> query = _readSet;

            if (isTracked == false)
                query = query.AsNoTracking();

            if (predicate != null)
                query = query.Where(predicate);

            if (includesList != null && !includesList.IsNullOrEmpty())
            {
                query = includesList.Aggregate(query, (current, include) => current.Include(include));
            }

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(Expression<Func<TEntity, bool>>? predicate,
            List<Expression<Func<TEntity, object>>>? includesList,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, bool? isTracked = false)
        {
            IQueryable<TEntity> query = _readSet;

            if (isTracked == false)
                query = query.AsNoTracking();

            if (predicate != null)
                query = query.Where(predicate);

            if (includesList != null && !includesList.IsNullOrEmpty())
            {
                query = includesList.Aggregate(query, (current, include) => current.Include(include));
            }

            if (orderBy != null)
                return await orderBy(query).FirstOrDefaultAsync();
            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<R>> GetProjectionAsync<R>(AutoMapper.IConfigurationProvider config,
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            List<Expression<Func<TEntity, object>>>? includesList = null, bool? isTracked = false,
            object? ProjectionParameters = null)
        {
            IQueryable<TEntity> query = _readSet;

            if (isTracked == false)
                query = query.AsNoTracking();

            if (predicate != null)
                query = query.Where(predicate);

            if (includesList != null && !includesList.IsNullOrEmpty())
            {
                query = includesList.Aggregate(query, (current, include) => current.Include(include));
            }

            if (orderBy != null)
                query = orderBy(query);

            return await query.ProjectTo<R>(config, ProjectionParameters).ToListAsync();
        }

        public async Task<R?> GetFirstProjectionAsync<R>(AutoMapper.IConfigurationProvider config,
            Expression<Func<TEntity, bool>>? predicate = null, object? ProjectionParameters = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            List<Expression<Func<TEntity, object>>>? includesList = null, bool? isTracked = false)
        {
            IQueryable<TEntity> query = _readSet;

            if (isTracked == false)
                query = query.AsNoTracking();

            if (predicate != null)
                query = query.Where(predicate);

            if (includesList != null && !includesList.IsNullOrEmpty())
            {
                query = includesList.Aggregate(query, (current, include) => current.Include(include));
            }

            if (orderBy != null)
                query = orderBy(query);

            return await query.ProjectTo<R>(config, ProjectionParameters).FirstOrDefaultAsync();
        }

        public IQueryable<TEntity> Include(Expression<Func<TEntity, object>> include)
        {
            return _readSet.Include(include);
        }

        public async Task<TEntity> GetFirstSpecificationAsync(BaseSpecifications<TEntity> specifications)
        {
            return await _readSet.ApplySpecification(specifications).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetSpecificationAsync(BaseSpecifications<TEntity> specifications)
        {
            return await _readSet.ApplySpecification(specifications).ToListAsync();
        }

        public async Task<IEnumerable<R>> GetSpecificationProjectionAsync<R>(BaseSpecifications<TEntity> specifications,
            AutoMapper.IConfigurationProvider config)
        {
            return await _readSet.ApplySpecification(specifications)
                .ProjectTo<R>(config)
                .ToListAsync();
        }

        public async Task<IEnumerable<R>> GetSpecificationProjectionAsync<R>(BaseSpecifications<TEntity> specifications,
            AutoMapper.IConfigurationProvider config, object projectionParameters)
        {
            return await _readSet.ApplySpecification(specifications)
                .ProjectTo<R>(config, projectionParameters)
                .ToListAsync();
        }

        public async virtual Task<TEntity?> GetByIdAsync(TId Id)
        {
            return await _writeSet.FindAsync(Id);
        }

        public async virtual Task<bool> SaveChanges()
        {
            try
            {
                await _writeCtx.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public virtual void Update(TEntity entity, params Expression<Func<TEntity, object?>>?[]? propertiesToIgnore)
        {
            var entry = _writeCtx.Entry(entity);

            // Ensure only one tracked instance
            if (entry.State == EntityState.Detached)
            {
                var key = _writeCtx.Model.FindEntityType(typeof(TEntity))?.FindPrimaryKey();
                if (key != null)
                {
                    var keyValues = key.Properties
                        .Select(p => p.PropertyInfo?.GetValue(entity))
                        .ToArray();

                    var tracked = _writeCtx.Set<TEntity>().Local
                        .FirstOrDefault(e => key.Properties
                            .Select(p => p.PropertyInfo?.GetValue(e))
                            .SequenceEqual(keyValues));

                    if (tracked != null)
                    {
                        _writeCtx.Entry(tracked).CurrentValues.SetValues(entity);
                        entry = _writeCtx.Entry(tracked);
                    }
                    else
                    {
                        _writeCtx.Attach(entity);
                        entry = _writeCtx.Entry(entity);
                    }
                }
            }

            entry.State = EntityState.Modified;

            // Unmark ignored properties as not modified
            if (propertiesToIgnore != null)
            {
                foreach (var property in propertiesToIgnore)
                {
                    if (property?.Body != null)
                    {
                        var propertyName = UtilityMethods.GetPropertyName(property.Body);
                        if (!string.IsNullOrEmpty(propertyName))
                        {
                            entry.Property(propertyName).IsModified = false;
                        }
                    }
                }
            }
        }


        public virtual void UpdateRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (_writeCtx.Entry(entity).State == EntityState.Detached)
                {
                    _writeCtx.Attach(entity);
                }

                _writeCtx.Entry(entity).State = EntityState.Modified;
            }
        }

        public Task<bool> Any(Expression<Func<TEntity, bool>> Predicate)
        {
            return _readSet.AnyAsync(Predicate);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (_writeCtx.Entry(entity).State == EntityState.Detached)
                    _writeCtx.Attach(entity);
                _writeCtx.Remove(entity);
            }
        }


        //public Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate, bool? isTracked = false)
        //{
        //    var query = isTracked == true ? _readSet : _readSet.AsNoTracking();
        //    return query.Where(predicate).FirstOrDefaultAsync();
        //}

        public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate, bool? isTracked = false)
        {
            var query = _readSet.Where(predicate);
            if (isTracked == false)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

        public IQueryable<TEntity> GetQuery()
        {
            return _readSet;
        }

        public IQueryable<TEntity> GetQueryFromWrite()
        {
            return _writeSet;
        }

        public async Task<JqueryDataTablesPagedResults<TReturn>> GetDataTableProjectionAsync<TReturn>(
            Func<IQueryable<TEntity>, IQueryable<TEntity>> query, JqueryDataTablesParameters table,
            IConfigurationProvider config, bool ShowAllData = false) where TReturn : class
        {
            try
            {
                var dbQuery = query(_readSet);

                dbQuery = SearchOptionsProcessor<TReturn, TEntity>.Apply(dbQuery, table.Columns);
                dbQuery = SortOptionsProcessor<TReturn, TEntity>.Apply(dbQuery, table);
                var size = await dbQuery.CountAsync();

                IQueryable<TReturn> itemsQuery;

                if (ShowAllData)
                {
                    itemsQuery = dbQuery.ProjectTo<TReturn>(config);
                }
                else
                {
                    itemsQuery = dbQuery
                        .Skip(table.Start / table.Length * table.Length)
                        .Take(table.Length)
                        .ProjectTo<TReturn>(config);
                }

                var items = await itemsQuery.AsNoTracking().ToArrayAsync();

                return new JqueryDataTablesPagedResults<TReturn>
                {
                    Items = items,
                    TotalSize = size
                };
            }
            catch (Exception ex)
            {
            }

            return new JqueryDataTablesPagedResults<TReturn>
            {
                TotalSize = 0
            };
        }

        public async Task<int> CountAsync(Expression<Func<TEntity, bool>>? predicate = null)
        {
            IQueryable<TEntity> query = _readSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.CountAsync();
        }


        #region Helpers

        public List<string> splitCommaString(string obj)
        {
            return new List<string>(obj.Split(',', StringSplitOptions.RemoveEmptyEntries));
        }

        public Task<IEnumerable<R>> GetProjectionAsync<R>(Microsoft.Extensions.Configuration.IConfigurationProvider config, Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, List<Expression<Func<TEntity, object>>>? includesList = null, bool? isTracked = false, object? ProjectionParameters = null)
        {
            throw new NotImplementedException();
        }

        public Task<R> GetFirstProjectionAsync<R>(Microsoft.Extensions.Configuration.IConfigurationProvider config, Expression<Func<TEntity, bool>>? predicate = null, object? ProjectionParameters = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, List<Expression<Func<TEntity, object>>>? includesList = null, bool? isTracked = false)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<R>> GetSpecificationProjectionAsync<R>(BaseSpecifications<TEntity> specifications, Microsoft.Extensions.Configuration.IConfigurationProvider config)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<R>> GetSpecificationProjectionAsync<R>(BaseSpecifications<TEntity> specifications, Microsoft.Extensions.Configuration.IConfigurationProvider config, object projectionParameters)
        {
            throw new NotImplementedException();
        }

        public Task<JqueryDataTablesPagedResults<TReturn>> GetDataTableProjectionAsync<TReturn>(Func<IQueryable<TEntity>, IQueryable<TEntity>> query, JqueryDataTablesParameters table, Microsoft.Extensions.Configuration.IConfigurationProvider config, bool ShowAllData) where TReturn : class
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}