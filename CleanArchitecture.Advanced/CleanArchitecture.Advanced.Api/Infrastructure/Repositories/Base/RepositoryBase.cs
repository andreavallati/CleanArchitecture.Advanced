﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using CleanArchitecture.Advanced.Common.Extensions;
using CleanArchitecture.Advanced.Api.Application.Interfaces.Caching;
using CleanArchitecture.Advanced.Api.Application.Interfaces.Repositories.Base;
using CleanArchitecture.Advanced.Api.Domain.Entities.Base;
using CleanArchitecture.Advanced.Api.Infrastructure.Context;
using CleanArchitecture.Advanced.Common.Constants;

namespace CleanArchitecture.Advanced.Api.Infrastructure.Repositories.Base
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : EntityBase
    {
        private const string Separator = "$_$";
        private const string KeyAll = "All";
        private const string KeyFirst = "First";

        protected virtual string KeyCachePrefix => $"{typeof(TEntity).Name}{Separator}";
        protected virtual string KeyCacheAll => $"{KeyCachePrefix}{Separator}{KeyAll}";
        protected virtual string KeyCacheFirst => $"{KeyCachePrefix}{Separator}{KeyFirst}";

        protected readonly LibraryContext _context;
        protected readonly ICustomMemoryCache _cache;

        protected RepositoryBase(LibraryContext context, ICustomMemoryCache cache)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var table = GetTable();
            return await _cache.GetSetItemsCacheAsync<TEntity>(async () => await table.AsNoTracking().ToListAsync(), KeyCacheAll);
        }

        public virtual async Task<IEnumerable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var key = $"{KeyCachePrefix}$GetWhere${predicate.ToCachableString()}";

            var table = GetTable();
            return await _cache.GetSetItemsCacheAsync<TEntity>(async () => await table.Where(predicate).AsNoTracking().ToListAsync(), key);
        }

        public virtual async Task<TEntity> GetByIdAsync(long id)
        {
            var table = GetTable();
            return await _cache.GetSetItemCacheAsync(async () => await table.FindAsync(id), KeyCachePrefix + id) ??
                   throw new InvalidOperationException($"Failed to fetch {typeof(TEntity).Name} from the database.");
        }

        public virtual async Task<TEntity> FirstEntityAsync()
        {
            var table = GetTable();
            return await _cache.GetSetItemCacheAsync(async () => await table.FirstOrDefaultAsync(), KeyCacheFirst) ??
                   throw new InvalidOperationException($"Failed to fetch {typeof(TEntity).Name} from the database.");
        }

        public virtual async Task<TEntity> FirstEntityAsync(Expression<Func<TEntity, bool>> expression)
        {
            var key = $"{KeyCachePrefix}${nameof(FirstEntityAsync)}${expression.ToCachableString()}";

            var table = GetTable();
            return await _cache.GetSetItemCacheAsync(async () => await table.FirstOrDefaultAsync(expression), key) ??
                   throw new InvalidOperationException($"Failed to fetch {typeof(TEntity).Name} from the database.");
        }

        public virtual async Task<IEnumerable<TField>> SelectAsync<TField>(Expression<Func<TEntity, TField>> expression) where TField : class
        {
            var key = $"{KeyCachePrefix}${nameof(SelectAsync)}${expression.ToCachableString()}";

            var table = GetTable();
            return await _cache.GetSetItemsCacheAsync<TField>(async () => await table.Select(expression).AsNoTracking().ToListAsync(), key);
        }

        public virtual async Task InsertAsync(TEntity entity)
        {
            // Invalidate cache since an item is added
            _cache.RemoveCacheBykey(KeyCacheAll);

            entity.CreationDate = DateTime.UtcNow.ToLocalTime();
            entity.CreationUser = CommonConstants.CreationUsername;

            await _context.Set<TEntity>().AddAsync(entity);
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            var existingEntity = await _context.Set<TEntity>().FindAsync(entity.Id);

            if (existingEntity is null)
            {
                throw new InvalidOperationException("Entity not found");
            }

            // Invalidate cache since an item is updated
            _cache.RemoveCacheBykey(KeyCacheAll);

            entity.ModificationDate = DateTime.UtcNow.ToLocalTime();
            entity.ModificationUser = CommonConstants.ModificationUsername;

            // Set only updated values to the existing entity
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
        }

        public virtual async Task DeleteAsync(long id)
        {
            var entity = await GetByIdAsync(id);
            if (entity is not null)
            {
                // Invalidate cache since an item is deleted
                _cache.RemoveCacheBykey(KeyCacheAll);

                _context.Set<TEntity>().Remove(entity);
            }
        }

        public virtual async Task CommitChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        private DbSet<TEntity> GetTable()
        {
            var table = _context.Set<TEntity>();
            return table;
        }
    }
}
