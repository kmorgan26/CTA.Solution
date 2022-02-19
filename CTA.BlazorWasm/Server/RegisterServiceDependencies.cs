using CTA.BlazorWasm.Shared.Context;
using Microsoft.EntityFrameworkCore;
using CTA.BlazorWasm.Shared.Interfaces;
using CTA.BlazorWasm.Shared.Repositories;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;

namespace CTA.BlazorWasm.Server
{
    public static class RegisterServiceDependencies
    {
        public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContextFactory<CtaContext>(
                options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("CTAConnectionString")));

            builder.Services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            builder.Services.AddScoped<ICorrTypeRepo, CorrTypeRepo>();
            builder.Services.AddScoped<ISubTypeRepo, SubTypeRepo>();
            builder.Services.AddScoped<IPocRepo, PocRepo>();
            builder.Services.AddScoped<IProjectRepo, ProjectRepo>();
            builder.Services.AddScoped<IStatusRepo, StatusRepo>();
            builder.Services.AddScoped<IToFromRepo, ToFromRepo>();
            builder.Services.AddScoped<ITrackingRepo, TrackingRepo>();
            builder.Services.AddScoped<IThreadRepo, ThreadRepo>();

            builder.Services.AddBlazorise(options =>
            {
                options.ChangeTextOnKeyPress = true;
            })
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            //builder.Services.AddControllers();
            //builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            return builder;
        }
    }
}
