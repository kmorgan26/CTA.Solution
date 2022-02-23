using CTA.BlazorWasm.Shared.Models;

namespace CTA.BlazorWasm.Client.Services
{
    public class StatusManager : ApiRepository<Status>
    {
        HttpClient http;

        public StatusManager(HttpClient _http) : base(_http, "status", "Id")
        {
            http = _http;
        }
    }
}