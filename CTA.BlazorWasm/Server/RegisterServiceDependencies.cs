﻿using CTA.BlazorWasm.Shared.Data;
using Microsoft.EntityFrameworkCore;
using CTA.BlazorWasm.Shared.Interfaces;
using CTA.BlazorWasm.Shared.Models;
using CTA.BlazorWasm.Server.Data;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity.UI.Services;
using CTA.BlazorWasm.Server.Services;
using CTA.BlazorWasm.Client.Services;
using System.Configuration;
using CTA.BlazorWasm.Shared.Models.SMTPModels;
using Microsoft.AspNetCore.Http.Features;

namespace CTA.BlazorWasm.Server
{
    public static class RegisterServiceDependencies
    {
        public static WebApplicationBuilder RegisterServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContextFactory<CtaContext>(
                options =>
                    options.UseSqlServer(builder.Configuration.GetConnectionString("CTAConnectionString")));

            builder.Services.AddDbContextFactory<IdentityFstssDbContext>(
                options => 
                    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

            builder.Services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityFstssDbContext>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["JwtIssuer"],
                        ValidAudience = builder.Configuration["JwtAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSecurityKey"]))
                    };
                });

            var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
            builder.Services.AddSingleton(emailConfig);
            builder.Services.AddScoped<ISmtpEmailSender, SmtpEmailSender>();

            builder.Services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });

            builder.Services.AddTransient<RepositoryEF<CorrespondenceType, CtaContext>>();
            builder.Services.AddTransient<RepositoryEF<CorrespondenceSubType, CtaContext>>();
            builder.Services.AddTransient<RepositoryEF<Poc, CtaContext>>();
            builder.Services.AddTransient<RepositoryEF<Project, CtaContext>>();

            builder.Services.AddTransient<RepositoryEF<Status, CtaContext>>();
            builder.Services.AddTransient<RepositoryEF<ToFrom, CtaContext>>();
            builder.Services.AddTransient<RepositoryEF<Tracking, CtaContext>>();
            builder.Services.AddTransient<RepositoryEF<TrackingThread, CtaContext>>();

            builder.Services.AddTransient<IEmailSender, EmailSender>();
            builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration);

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddControllers()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            return builder;
        }
    }
}
