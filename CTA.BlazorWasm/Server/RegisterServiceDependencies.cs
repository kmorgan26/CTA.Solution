using CTA.BlazorWasm.Shared.Data;
using Microsoft.EntityFrameworkCore;
using CTA.BlazorWasm.Shared.Interfaces;
using CTA.BlazorWasm.Shared.Models;
using CTA.BlazorWasm.Server.Data;

namespace CTA.BlazorWasm.Server
{
    public static class RegisterServiceDependencies
    {
        public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContextFactory<CtaContext>(
                options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("CTAConnectionString")));

            builder.Services.AddTransient<RepositoryEF<CorrespondenceType, CtaContext>>();
            builder.Services.AddTransient<RepositoryEF<CorrespondenceSubType, CtaContext>>();
            builder.Services.AddTransient<RepositoryEF<Poc, CtaContext>>();
            builder.Services.AddTransient<RepositoryEF<Project, CtaContext>>();

            builder.Services.AddTransient<RepositoryEF<Status, CtaContext>>();
            builder.Services.AddTransient<RepositoryEF<ToFrom, CtaContext>>();
            builder.Services.AddTransient<RepositoryEF<Tracking, CtaContext>>();
            builder.Services.AddTransient<RepositoryEF<TrackingThread, CtaContext>>();

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            return builder;
        }
    }
}
