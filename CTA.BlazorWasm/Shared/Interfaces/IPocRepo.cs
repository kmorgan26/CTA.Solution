using CTA.BlazorWasm.Shared.Models;

namespace CTA.BlazorWasm.Shared.Interfaces
{
    public interface IPocRepo : IAsyncRepository<Poc>
    {
        Task<IEnumerable<Poc>> GetActiveAsync();
    }
}
