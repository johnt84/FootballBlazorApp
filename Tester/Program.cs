using FootballEngine.API;
using FootballShared.Models;
using FootballEngine.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http;

var builder = new ConfigurationBuilder()
                               .SetBasePath($"{Directory.GetCurrentDirectory()}/../../..")
                               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

var config = builder.Build();

var httpClient = new HttpClient();

httpClient.BaseAddress = new Uri(config["FootballDataAPIUrl"]);
httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", config["PrivateToken"]);

var footballEngineInput = new FootballEngineInput()
{
    FootballDataAPIUrl = config["FootballDataAPIUrl"].ToString(),
    Competition = config["Competition"].ToString(),
    HasGroups = Convert.ToBoolean(config["HasGroups"].ToString()),
    LeagueName = config["LeagueName"].ToString(),
    APIToken = config["APIToken"].ToString(),
    HoursUntilRefreshCache = Convert.ToInt32(config["HoursUntilRefreshCache"].ToString()),
};

var httpAPIClient = new HttpAPIClient(httpClient, footballEngineInput);

var footballDataService = new FootballDataService(httpAPIClient, new FootballDataState(), footballEngineInput);

var fixturesAndResultsByDays = await footballDataService.GetFixturesAndResultsByDays();

var groupsOrLeagueTable = await footballDataService.GetGroupsOrLeagueTable();

var teams = await footballDataService.GetTeams();

var team = await footballDataService.GetTeam(67);
