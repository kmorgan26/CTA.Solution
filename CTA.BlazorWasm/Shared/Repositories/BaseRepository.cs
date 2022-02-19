using CTA.BlazorWasm.Shared.Context;
using CTA.BlazorWasm.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTA.BlazorWasm.Shared.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : class
    {
        protected readonly IDbContextFactory<CtaContext> _dbContextFactory;

        public BaseRepository(IDbContextFactory<CtaContext> dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            using(var context = _dbContextFactory.CreateDbContext())
            {
                return await context.Set<T>().FindAsync(id);
            }
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return await context.Set<T>().ToListAsync();
            }
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return await context.Set<T>().ToListAsync();
            }
        }
        public async virtual Task<IReadOnlyList<T>> GetPagedReponseAsync(int page, int size)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                return await context.Set<T>().Skip((page - 1) * size).Take(size).AsNoTracking().ToListAsync();
            }
        }

        public async Task<T> AddAsync(T entity)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }

        public async Task UpdateAsync(T entity)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }                
        }

        public async Task DeleteAsync(T entity)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}
