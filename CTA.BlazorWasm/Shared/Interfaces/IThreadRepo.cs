using CTA.BlazorWasm.Shared.Models;

namespace CTA.BlazorWasm.Shared.Interfaces
{
    public interface IThreadRepo : IAsyncRepository<TrackingThread>
    {
        Task<IEnumerable<TrackingThread>> GetTrackingThreadsByProjectIdAsync(int id);
    }
}
