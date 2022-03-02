using FootballEngine.API;
using FootballShared.Models;
using FootballEngine.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net.Http;
using System.Linq;

var builder = new ConfigurationBuilder()
                               .SetBasePath($"{Directory.GetCurrentDirectory()}/../../..")
                               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

var config = builder.Build();

var footballEngineInput = new FootballEngineInput()
{
    FootballDataAPIUrl = config["FootballDataAPIUrl"].ToString(),
    Competition = config["Competition"].ToString(),
    HasGroups = Convert.ToBoolean(config["HasGroups"].ToString()),
    LeagueName = config["LeagueName"].ToString(),
    APIToken = config["APIToken"].ToString(),
    HoursUntilRefreshCache = Convert.ToInt32(config["HoursUntilRefreshCache"].ToString()),
};

var httpClient = new HttpClient();

httpClient.BaseAddress = new Uri(config["FootballDataAPIUrl"]);
httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Token", footballEngineInput.APIToken);

var httpAPIClient = new HttpAPIClient(httpClient, footballEngineInput);

var footballDataState = new FootballDataState();

var footballDataService = new FootballDataService(httpAPIClient, footballDataState, footballEngineInput);

var fixturesAndResultsByDays = await footballDataService.GetFixturesAndResultsByDaysAsync();

var groupsOrLeagueTable = await footballDataService.GetGroupsOrLeagueTableAsync();

var teams = await footballDataService.GetTeamsAsync();

int newcastleTeamId = teams
                        .Where(x => x.Name == "Newcastle")
                        .Select(x => x.TeamID)
                        .FirstOrDefault();

var team = await footballDataService.GetTeamAsync(newcastleTeamId);

Console.WriteLine("Press any key to continue");
Console.ReadLine();
