﻿using CTA.BlazorWasm.Shared.Context;
using CTA.BlazorWasm.Shared.Entities;
using CTA.BlazorWasm.Shared.Interfaces;
using CTA.BlazorWasm.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CTA.BlazorWasm.Shared.Repositories
{
    public class ThreadRepo : BaseRepository<TrackingThread>, IThreadRepo
    {
        public ThreadRepo(IDbContextFactory<CtaContext> dbContext) : base(dbContext) { }

        public async Task<IEnumerable<TrackingThread>> GetTrackingThreads(int id)
        {
            using(var _dbContext = _dbContextFactory.CreateDbContext())
            {

                var items = await _dbContext.TrackingThreads
                    .Include(i => i.Project)
                .Where(i => i.ProjectId == id)
                .ToListAsync();

                return items;
            }
            
        }
    }
}
