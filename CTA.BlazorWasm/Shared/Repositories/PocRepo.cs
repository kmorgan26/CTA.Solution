using CTA.BlazorWasm.Shared.Context;
using CTA.BlazorWasm.Shared.Entities;
using CTA.BlazorWasm.Shared.Interfaces;
using CTA.BlazorWasm.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CTA.BlazorWasm.Shared.Repositories
{
    public class PocRepo : BaseRepository<Poc>, IPocRepo
    {
        public PocRepo(IDbContextFactory<CtaContext> dbContext) : base(dbContext) { }

        public async Task<IEnumerable<Poc>> GetActiveAsync()
        {
            using(var _dbContext = _dbContextFactory.CreateDbContext())
            {
                var items = await _dbContext.Pocs
                    .Where(i => i.IsActive == true)
                    .ToListAsync();

                return items;
            }
        }
    }
}
