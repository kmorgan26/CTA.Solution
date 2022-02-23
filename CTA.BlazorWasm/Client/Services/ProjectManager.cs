using CTA.BlazorWasm.Shared.Models;

namespace CTA.BlazorWasm.Client.Services
{
    public class ProjectManager : ApiRepository<Project>
    {
        HttpClient http;

        public ProjectManager(HttpClient _http) : base(_http, "project", "Id")
        {
            http = _http;
        }
    }
}