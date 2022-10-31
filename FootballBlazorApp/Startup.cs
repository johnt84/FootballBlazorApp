using FootballBlazorApp.Data;
using FootballEngine.API;
using FootballEngine.API.Interfaces;
using FootballEngine.Services;
using FootballEngine.Services.Interfaces;
using FootballShared.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;

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

            var availableCompetitions = Configuration.GetSection("AvailableCompetitions").Get<List<Competition>>();

            var defaultCompetition = availableCompetitions
                                        .Where(x => x.CompetitionCode == Configuration["DefaultCompetitionCode"].ToString())
                                        .FirstOrDefault();

            var footballEngineInput = new FootballEngineInput()
            {
                FootballDataAPIUrl = Configuration["FootballDataAPIUrl"].ToString(),
                AvailableCompetitions = availableCompetitions,
                SelectedCompetition = defaultCompetition ?? availableCompetitions.First(),
                APIToken = Configuration["APIToken"].ToString(),
                HoursUntilRefreshCache = Convert.ToInt32(Configuration["HoursUntilRefreshCache"].ToString()),
                MinutesUntilRefreshPlayerSearchCache = Convert.ToInt32(Configuration["MinutesUntilRefreshPlayerSearchCache"].ToString()),
                ForceCacheRefreshInput = new ForceCacheRefreshInput(),
            };

            services.AddSingleton(footballEngineInput);
            services.AddHttpClient<IHttpAPIClient, HttpAPIClient>();
            services.AddScoped<ITimeZoneOffsetService, TimeZoneOffsetService>();
            services.AddScoped<IFootballDataService, FootballDataService>();
            services.AddScoped<FootballDataState>();
            services.AddScoped<IPlayerSearchCacheService, PlayerSearchCacheService>();
            services.AddScoped<PlayerSearchState>();
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
