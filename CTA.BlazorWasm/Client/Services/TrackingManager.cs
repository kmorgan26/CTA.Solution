using CTA.BlazorWasm.Shared.Filters;
using CTA.BlazorWasm.Shared.Models;

namespace CTA.BlazorWasm.Client.Services
{
    public class TrackingManager : ApiRepository<Tracking>
    {
        HttpClient http;

        public TrackingManager(HttpClient _http) : base(_http, "tracking", "Id")
        {
            http = _http;
        }
        
    }
}