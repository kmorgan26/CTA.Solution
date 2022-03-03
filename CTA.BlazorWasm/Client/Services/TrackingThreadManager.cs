using CTA.BlazorWasm.Shared.Models;
using CTA.BlazorWasm.Shared.Responses;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;

namespace CTA.BlazorWasm.Client.Services
{
    public class TrackingThreadManager : ApiRepository<TrackingThread>
    {
        HttpClient http;

        public TrackingThreadManager(HttpClient _http) : base(_http, "trackingthread", "Id")
        {
            http = _http;
        }

        public async Task<IEnumerable<TrackingThread>> GetThreadsByProjectId(object id)
        {
            try
            {
                var arg = WebUtility.HtmlEncode(id.ToString());
                var url = "trackingthread" + "/project/" + arg;
                var result = await http.GetAsync(url);
                result.EnsureSuccessStatusCode();
                string responseBody = await result.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<PagedResponse<TrackingThread>>(responseBody);
                if (response!.Success)
                    return response.Data;
                else
                    return new List<TrackingThread>();
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}