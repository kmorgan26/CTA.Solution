using CTA.BlazorWasm.Shared.Entities;

namespace CTA.BlazorWasm.Shared.Interfaces
{
    public interface IPocRepo : IAsyncRepository<Poc>
    {
        Task<IEnumerable<Poc>> GetActiveAsync();
    }
}
