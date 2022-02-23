using CTA.BlazorWasm.Shared.Data;
using CTA.BlazorWasm.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTA.BlazorWasm.Shared.Repositories
{
    public class BaseRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : class
    {
        protected readonly IDbContextFactory<CtaContext> _dbContextFactory;

        public BaseRepository(IDbContextFactory<CtaContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            using(var context = _dbContextFactory.CreateDbContext())
            {
                return await context.Set<TEntity>().FindAsync(id);
            }
        }

        public async Task<IReadOnlyList<TEntity>> ListAllAsync()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return await context.Set<TEntity>().ToListAsync();
            }
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return await context.Set<TEntity>().ToListAsync();
            }
        }
        public async virtual Task<IReadOnlyList<TEntity>> GetPagedReponseAsync(int page, int size)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return await context.Set<TEntity>().Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync();
            }
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                await context.Set<TEntity>().AddAsync(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }

        public async Task UpdateAsync(TEntity entity)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }                
        }

        public async Task DeleteAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        

        public Task<bool> DeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        Task<TEntity> IAsyncRepository<TEntity>.UpdateAsync(TEntity entityToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
