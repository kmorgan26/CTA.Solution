using CTA.BlazorWasm.Shared.Entities;

namespace CTA.BlazorWasm.Shared.Interfaces
{
    public interface IThreadRepo : IAsyncRepository<TrackingThread>
    {
        Task<IEnumerable<TrackingThread>> GetTrackingThreads(int id);
    }
}
