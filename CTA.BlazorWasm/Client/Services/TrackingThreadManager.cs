using CTA.BlazorWasm.Shared.Models;

namespace CTA.BlazorWasm.Client.Services
{
    public class TrackingThreadManager : ApiRepository<TrackingThread>
    {
        HttpClient http;

        public TrackingThreadManager(HttpClient _http) : base(_http, "trackingthread", "Id")
        {
            http = _http;
        }
    }
}