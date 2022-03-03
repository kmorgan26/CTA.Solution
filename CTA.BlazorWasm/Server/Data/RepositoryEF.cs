using CTA.BlazorWasm.Client.ViewModels.Shared;
using CTA.BlazorWasm.Shared.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CTA.BlazorWasm.Server.Data
{
    public class RepositoryEF<TEntity, TDataContext> : IAsyncRepository<TEntity>
        where TEntity : class
        where TDataContext : DbContext
    {
        protected readonly TDataContext context;
        internal DbSet<TEntity> dbSet;

        public RepositoryEF(TDataContext dataContext)
        {
            context = dataContext;
            context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            dbSet = context.Set<TEntity>();
        }
        public virtual async Task<bool> DeleteAsync(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
            return await context.SaveChangesAsync() >= 1;
        }
        public virtual async Task<bool> DeleteAsync(object id)
        {
            TEntity entityToDelete = await dbSet.FindAsync(id);
            return await DeleteAsync(entityToDelete);
        }        
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync(PaginationFilter? paginationFilter = null)
        {
            await Task.Delay(0);

            if (paginationFilter == null)
            {
                return dbSet;
            }
            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

            return await dbSet
                .Take(paginationFilter.PageSize)
                .Skip(skip).ToListAsync();
        }        
        public virtual async Task<TEntity> GetByIdAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            await context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity entityToUpdate)
        {
            var dbset = context.Set<TEntity>();
            dbset.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return entityToUpdate;
        }

        public Task<IReadOnlyList<TEntity>> GetPagedReponseAsync(int page, int size)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<TEntity>> ListAllAsync()
        {
            throw new NotImplementedException();
        }

        
    }
}
