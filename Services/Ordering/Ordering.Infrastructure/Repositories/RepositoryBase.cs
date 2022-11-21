using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Common;
using Ordering.Infrastructure.Persistence;

namespace Ordering.Infrastructure.Repositories
{
    public class RepositoryBase<TEntity> : IAsyncRepository<TEntity>
        where TEntity : EntityBase
    {
        protected readonly OrderContext orderContext;

        public RepositoryBase(OrderContext orderContext)
        {
            this.orderContext = orderContext;
        }

        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
        {
            return await orderContext.Set<TEntity>().ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await orderContext.Set<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            IQueryable<TEntity> query = orderContext.Set<TEntity>();
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (!string.IsNullOrWhiteSpace(includeString))
            {
                query = query.Include(includeString);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, List<Expression<Func<TEntity, object>>> includes = null, bool disableTracking = true)
        {
            IQueryable<TEntity> query = orderContext.Set<TEntity>();
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await orderContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            orderContext.Set<TEntity>().Add(entity);
            await orderContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            orderContext.Entry(entity).State = EntityState.Modified;
            await orderContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            orderContext.Set<TEntity>().Remove(entity);
            await orderContext.SaveChangesAsync();
        }
    }
}

