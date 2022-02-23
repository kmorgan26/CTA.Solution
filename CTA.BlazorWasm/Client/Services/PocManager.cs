using CTA.BlazorWasm.Shared.Models;

namespace CTA.BlazorWasm.Client.Services
{
    public class PocManager : ApiRepository<Poc>
    {
        HttpClient http;

        public PocManager(HttpClient _http) : base(_http, "poc", "Id")
        {
            http = _http;
        }
    }
}