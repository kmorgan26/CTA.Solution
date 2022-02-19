using CTA.BlazorWasm.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

// other usings
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.Services
          .AddBlazorise(options =>
          {
              options.ChangeTextOnKeyPress = true;
          })
          .AddBootstrapProviders()
          .AddFontAwesomeIcons();

        builder.Services.AddSingleton(new HttpClient
        {
            BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
        });

        builder.RootComponents.Add<App>("#app");

        var host = builder.Build();

        await host.RunAsync();
    }
}