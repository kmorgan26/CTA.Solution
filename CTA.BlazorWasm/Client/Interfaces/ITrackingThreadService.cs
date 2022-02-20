using CTA.BlazorWasm.Client.ViewModels.Threads;
using CTA.BlazorWasm.Shared.Entities;

namespace CTA.BlazorWasm.Client.Interfaces
{
    public interface ITrackingThreadService
    {
        Task<IEnumerable<TrackingThreadVm>> GetTrackingThreads();
    }
}
