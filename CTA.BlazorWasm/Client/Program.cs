using Blazored.LocalStorage;
using CTA.BlazorWasm.Client;
using CTA.BlazorWasm.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.Services.AddMudServices();

        builder.Services.AddSingleton(new HttpClient
        {
            BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
        });

        builder.RootComponents.Add<App>("#app");
        builder.Services.AddScoped<PocManager>();
        builder.Services.AddScoped<ProjectManager>();
        builder.Services.AddScoped<CorrespondenceTypeManager>();
        builder.Services.AddScoped<StatusManager>();
        builder.Services.AddScoped<ToFromManager>();
        builder.Services.AddScoped<TrackingManager>();
        builder.Services.AddScoped<TrackingThreadManager>();

        builder.Services.AddBlazoredLocalStorage();
        builder.Services.AddAuthorizationCore();
        builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
        builder.Services.AddScoped<IAuthService, AuthService>();
        var host = builder.Build();

        await host.RunAsync();
    }
}