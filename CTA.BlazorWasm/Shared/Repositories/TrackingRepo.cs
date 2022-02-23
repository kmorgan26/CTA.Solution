using CTA.BlazorWasm.Shared.Data;
using CTA.BlazorWasm.Shared.Models;
using CTA.BlazorWasm.Shared.Filters;
using CTA.BlazorWasm.Shared.Interfaces;
using CTA.BlazorWasm.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CTA.BlazorWasm.Shared.Repositories 
{ 
    public class TrackingRepo : BaseRepository<Tracking>, ITrackingRepo
    {
        public TrackingRepo(IDbContextFactory<CtaContext> dbContext) : base(dbContext) { }

        public async Task<IEnumerable<Tracking>> GetTrackings(int id)
        {
            using (var _dbContext = _dbContextFactory.CreateDbContext())
            {
                var items = await _dbContext.Trackings
                .Include(i => i.ToFrom)
                .Include(i => i.Status)
                .Include(i => i.Thread)
                .ThenInclude(i => i.Project)
                .Where(i => i.ThreadId == id)
                .ToListAsync();

                return items;
            }
        }
        public async Task<Tracking> GetTracking(int id)
        {
            using (var _dbContext = _dbContextFactory.CreateDbContext())
            {
                var item = await _dbContext.Trackings
                .Include(i => i.ToFrom)
                .Include(i => i.Poc)
                .Include(i => i.CorrespondenceType)
                .Include(i => i.Status)
                .Include(i => i.Thread)
                .ThenInclude(i => i.Project)
                .Where(i => i.Id == id)
                .FirstOrDefaultAsync();

                return item;
            }
        }
        public async Task<IEnumerable<Tracking>> GetFilteredTrackingsAsync(TrackingFilter filter)
        {
            using (var _dbContext = _dbContextFactory.CreateDbContext())
            {
                var result = _dbContext.Trackings
                    .Include(i => i.CorrespondenceType)
                        .ThenInclude(j => j.CorrespondenceSubType)
                    .Include(i => i.Thread)
                        .ThenInclude(j => j.Project)
                    .Include(i => i.Status)
                    .Include(i => i.Poc)
                    .Include(i => i.ToFrom)
                    .AsNoTracking();

                if(filter.StatusId != null)
                    result = result.Where(i => i.StatusId == filter.StatusId);

                if (filter.ToFromId != null)
                    result = result.Where(i => i.ToFromId == filter.ToFromId);

                if (filter.CorrTypeId != null)
                    result = result.Where(i => i.CorrespondenceTypeId == filter.CorrTypeId);

                if (filter.PocId != null)
                    result = result.Where(i => i.PocId == filter.PocId);

                if (filter.TypeIds != null)
                    result = result.Where(i => filter.TypeIds.Contains(i.CorrespondenceType.CorrespondenceSubTypeId));

                if (filter.StatusIds != null)
                    result = result.Where(i => filter.StatusIds.Contains(i.StatusId));

                if (filter.PocIds != null)
                    result = result.Where(i => filter.PocIds.Contains(i.PocId));

                if (filter.ToFromIds != null)
                    result = result.Where(i => filter.ToFromIds.Contains(i.ToFromId));

                if(filter.StartDate != null && filter.EndDate != null)
                    result = result.Where(i => i.SentOrReceived >= filter.StartDate && i.SentOrReceived <= filter.EndDate);
                
                if(filter.ThreadId != 0)
                    result = result.Where(i => i.ThreadId == filter.ThreadId);

                if(filter.SubjectText != null)
                    result = result.Where(i => i.Subject.Contains(filter.SubjectText));

                if (filter.CommentsText != null)
                    result = result.Where(i => i.Comments.Contains(filter.CommentsText));

                return await result
                    .OrderBy(i => i.ThreadId)
                    .ToListAsync();
            }
        }
    }
}
