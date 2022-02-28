using FootballBlazorApp.Data;
using FootballEngine.API;
using FootballEngine.Services;
using FootballEngine.Services.Interfaces;
using FootballShared.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace FootballBlazorApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            var footballEngineInput = new FootballEngineInput()
            {
                FootballDataAPIUrl = Configuration["FootballDataAPIUrl"].ToString(),
                Competition = Configuration["Competition"].ToString(),
                HasGroups = Convert.ToBoolean(Configuration["HasGroups"].ToString()),
                LeagueName = Configuration["LeagueName"].ToString(),
                APIToken = Configuration["APIToken"].ToString(),
                HoursUntilRefreshCache = Convert.ToInt32(Configuration["HoursUntilRefreshCache"].ToString()),
            };

            services.AddSingleton(footballEngineInput);
            services.AddHttpClient<IHttpAPIClient, HttpAPIClient>();
            services.AddScoped<ITimeZoneOffsetService, TimeZoneOffsetService>();
            services.AddSingleton<IFootballDataService, FootballDataService>();
            services.AddSingleton<FootballDataState>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
