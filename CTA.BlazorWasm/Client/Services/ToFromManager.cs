using CTA.BlazorWasm.Shared.Models;

namespace CTA.BlazorWasm.Client.Services
{
    public class ToFromManager : ApiRepository<ToFrom>
    {
        HttpClient http;

        public ToFromManager(HttpClient _http) : base(_http, "tofrom", "Id")
        {
            http = _http;
        }
    }
}