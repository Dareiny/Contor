﻿using System;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected DbContext DbContext { get; }
        protected DbSet<TEntity> DbSet { get; }

        public Repository(DbContext context)
        {
            DbContext = context;
            DbSet = DbContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await DbSet.AddAsync(entity);
            await DbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var entity = await GetByIdAsync(id, cancellationToken);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Запись с id {id} не найдена.");
            }

            DbSet.Remove(entity);
            await DbContext.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetAll()
        {
            return DbSet.AsNoTracking();
        }

        public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await DbSet.FindAsync(id, cancellationToken);
        }

        public IQueryable<TEntity> GetFiltered(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));
            return DbSet.Where(predicate).AsNoTracking();
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            DbSet.Update(entity);
            await DbContext.SaveChangesAsync();
        }
    }
}