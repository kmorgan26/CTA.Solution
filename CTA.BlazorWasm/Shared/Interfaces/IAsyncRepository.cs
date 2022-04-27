using CTA.BlazorWasm.Shared.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTA.BlazorWasm.Shared.Interfaces
{
    public interface IAsyncRepository<TEntity> where TEntity : class
    {
        Task<bool> DeleteAsync(TEntity entityToDelete);
        Task<bool> DeleteAsync(object id);
        Task<IEnumerable<TEntity>> GetAllAsync(PaginationFilter? pagination = null);
        Task<TEntity> GetByIdAsync(object id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entityToUpdate);
        Task<IReadOnlyList<TEntity>> ListAllAsync();
        Task<IReadOnlyList<TEntity>> GetPagedReponseAsync(int page, int size);
    }
}
