using CTA.BlazorWasm.Shared.Models;

namespace CTA.BlazorWasm.Client.Services
{
    public class CorrespondenceTypeManager : ApiRepository<CorrespondenceType>
    {
        HttpClient http;

        public CorrespondenceTypeManager(HttpClient _http) : base(_http, "correspondenceType", "Id")
        {
            http = _http;
        }
    }
}