using CTA.BlazorWasm.Shared.Filters;
using CTA.BlazorWasm.Shared.Models;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http.Json;
using CTA.BlazorWasm.Shared.Responses;

namespace CTA.BlazorWasm.Client.Services
{
    public class TrackingManager : ApiRepository<Tracking>
    {
        HttpClient http;

        public TrackingManager(HttpClient _http) : base(_http, "tracking", "Id")
        {
            http = _http;
        }
        public async Task<IEnumerable<Tracking>> GetTrackingsByThreadId(object id)
        {
            try
            {
                var arg = WebUtility.HtmlEncode(id.ToString());
                var url = $"tracking/thread/{arg}";
                var result = await http.GetAsync(url);
                result.EnsureSuccessStatusCode();
                string responseBody = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<PagedResponse<Tracking>>(responseBody);
                if (response!.Success)
                    return response.Data;
                else
                    return new List<Tracking>();
            }
            catch (Exception)
            {
                return new List<Tracking>();
            }
        }

        public async Task<IEnumerable<Tracking>> GetTrackingsFiltered(string encodedFilter)
        {
            try
            {
                var arg = WebUtility.HtmlEncode(encodedFilter.ToString());
                var url = $"tracking/filter/{arg}";
                var result = await http.GetAsync(url);
                result.EnsureSuccessStatusCode();
                string responseBody = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<PagedResponse<Tracking>>(responseBody);
                if (response!.Success)
                    return response.Data;
                else
                    return new List<Tracking>();
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}