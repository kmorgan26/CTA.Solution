using CTA.BlazorWasm.Shared.Models;
using CTA.BlazorWasm.Shared.Filters;

namespace CTA.BlazorWasm.Shared.Interfaces
{
    public interface ITrackingRepo : IAsyncRepository<Tracking>
    {
        Task<IEnumerable<Tracking>> GetTrackings(int id);
        Task<IEnumerable<Tracking>> GetFilteredTrackingsAsync(TrackingFilter filter);
        Task<Tracking> GetTracking(int id);
    }
}
