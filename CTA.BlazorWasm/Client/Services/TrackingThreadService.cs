using AutoMapper;
using CTA.BlazorWasm.Client.Interfaces;
using CTA.BlazorWasm.Client.ViewModels.Threads;
using CTA.BlazorWasm.Shared.Entities;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CTA.BlazorWasm.Client.Services
{
    public class TrackingThreadService : ITrackingThreadService
    {
        private readonly HttpClient _httpClient;
        public TrackingThreadService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<TrackingThreadVm>> GetTrackingThreads()
        {
            var threads = await _httpClient.GetFromJsonAsync<TrackingThread[]>("api/trackingthread");
            return Mapping.Mapper.Map<List<TrackingThreadVm>>(threads);
        }
    }
}
