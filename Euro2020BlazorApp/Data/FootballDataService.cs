using Euro2020BlazorApp.API;
using Euro2020BlazorApp.Models;
using Euro2020BlazorApp.Models.FootballData;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Euro2020BlazorApp.Data
{
    public class FootballDataService : IFootballDataService
    {
        private readonly HttpAPIClient _httpAPIClient;
        private readonly ITimeZoneOffsetService _timeZoneOffsetService;
        private readonly IConfiguration _configuration;
        private readonly FootballDataState _footballDataState;

        private DateTime GetCompetitionStateDate(DateTime startDate) => !_footballDataState.CompetitionStartDate.HasValue
                                                                            ? startDate
                                                                            : _footballDataState.CompetitionStartDate.Value;

        private bool CacheRequiresRefresh => DateTime.UtcNow > _footballDataState.CompetitionStartDate
                                            && DateTime.UtcNow > _footballDataState
                                                        .LastRefreshTime
                                                        .AddHours(Convert.ToInt32(_configuration["HoursUntilRefreshCache"].ToString()));

        public FootballDataService(HttpAPIClient httpAPIClient, ITimeZoneOffsetService timeZoneOffsetService
                                    , IConfiguration configuration, FootballDataState footballDataState)
        {
            _httpAPIClient = httpAPIClient;
            _timeZoneOffsetService = timeZoneOffsetService;
            _configuration = configuration;
            _footballDataState = footballDataState;
        }

        public async Task<List<Group>> GetGroups()
        {
            bool groupsCached = _footballDataState != null && _footballDataState.Groups != null;
            bool groupsCacheRequiresRefresh = !groupsCached
                                    || CacheRequiresRefresh;


            if (!groupsCacheRequiresRefresh)
            {
                return _footballDataState.Groups;
            }
            
            var footballDataStandings = await GetFootballDataStandings();

            var groupService = new GroupService(footballDataStandings);
            var groups = groupService.GetGroups();

            var fixturesAndGroups = await GetFixturesAndResultsByGroups(groups);

            _footballDataState.CompetitionStartDate = GetCompetitionStateDate(footballDataStandings.season.startDate);
            _footballDataState.Groups = fixturesAndGroups;
            _footballDataState.LastRefreshTime = DateTime.UtcNow;

            return fixturesAndGroups;
        }

        public async Task<List<Models.Team>> GetTeams()
        {
            bool teamsCached = _footballDataState != null && _footballDataState.Teams != null;
            bool teamsCacheRequiresRefresh = !teamsCached
                                                || CacheRequiresRefresh;

            if (!teamsCacheRequiresRefresh)
            {
                return _footballDataState.Teams;
            }

            var footballDataTeams = await GetFootballDataTeams();

            var teamService = new TeamService(footballDataTeams);

            var teams = teamService.GetTeams();

            _footballDataState.Teams = teams;
            _footballDataState.CompetitionStartDate = GetCompetitionStateDate(footballDataTeams.season.startDate);
            _footballDataState.LastRefreshTime = DateTime.UtcNow;

            return teams;
        }

        public async Task<Models.Team> GetTeam(int teamID)
        {
            var footballDataTeam = await GetFootballDataTeam(teamID);

            var teamService = new TeamService(footballDataTeam);
            var team = teamService.GetTeam();

            FootballDataModel footballDataMatches = null;

            bool footballDataModelCached = _footballDataState != null && _footballDataState.FootballDataModel != null;
            bool footballDataModelCacheRequiresRefresh = !footballDataModelCached
                                                || CacheRequiresRefresh;

            if (!footballDataModelCacheRequiresRefresh)
            {
                footballDataMatches = _footballDataState.FootballDataModel;
            }
            else
            {
                footballDataMatches = await GetFootballDataMatches();
            }

            var fixtureAndResultService = new FixtureAndResultService(footballDataTeam, footballDataMatches, _timeZoneOffsetService);
            return await fixtureAndResultService.GetFixturesAndResultsByTeam(team);
        }

        public async Task<List<FixturesAndResultsByDay>> GetFixturesAndResultsByDays()
        {
            bool fixturesAndResultsCached = _footballDataState != null && _footballDataState.FixturesAndResultsByDays != null;
            bool fixturesAndResultsCachedCacheRequiresRefresh = !fixturesAndResultsCached
                                                                    || CacheRequiresRefresh;

            if (!fixturesAndResultsCachedCacheRequiresRefresh)
            {
                return _footballDataState.FixturesAndResultsByDays;
            }

            var footballDataMatches = await GetFootballDataMatches();

            var fixtureAndResultService = new FixtureAndResultService(footballDataMatches, _timeZoneOffsetService);

            var fixturesAndResultsByDay = await fixtureAndResultService.GetFixturesAndResultsByDay();

            _footballDataState.FixturesAndResultsByDays = fixturesAndResultsByDay;
            _footballDataState.FootballDataModel = footballDataMatches;

            var startDate = footballDataMatches != null && footballDataMatches.matches != null
                            ? footballDataMatches.matches.ToList().First().season.startDate
                            : DateTime.MinValue;

            _footballDataState.CompetitionStartDate = GetCompetitionStateDate(startDate);

            _footballDataState.LastRefreshTime = DateTime.UtcNow;

            return fixturesAndResultsByDay;
        }

        public async Task<List<Group>> GetFixturesAndResultsByGroups(List<Group> groups)
        {
            if (_footballDataState != null && _footballDataState.FixturesAndResultsByGroups != null)
            {
                return _footballDataState.FixturesAndResultsByGroups;
            }

            var footballDataMatches = await GetFootballDataMatches();

            var fixtureAndResultService = new FixtureAndResultService(footballDataMatches, _timeZoneOffsetService);

            var fixturesAndResultsByGroups = await fixtureAndResultService.GetFixturesAndResultsByGroups(groups);

            _footballDataState.FixturesAndResultsByGroups = fixturesAndResultsByGroups;
            _footballDataState.FootballDataModel = footballDataMatches;
            _footballDataState.LastRefreshTime = DateTime.UtcNow;

            return fixturesAndResultsByGroups;
        }

        private async Task<FootballDataModel> GetFootballDataMatches()
        {
            string json = await _httpAPIClient.Get($"{_httpAPIClient._Client.BaseAddress}competitions/{_configuration["Competition"]}/matches/");
            return JsonSerializer.Deserialize<FootballDataModel>(json);
        }

        private async Task<FootballDataModel> GetFootballDataStandings()
        {
            string json = await _httpAPIClient.Get($"{_httpAPIClient._Client.BaseAddress}competitions/{_configuration["Competition"]}/standings/");
            return JsonSerializer.Deserialize<FootballDataModel>(json);
        }

        private async Task<Teams> GetFootballDataTeams()
        {
            string json = await _httpAPIClient.Get($"{_httpAPIClient._Client.BaseAddress}competitions/{_configuration["Competition"]}/teams/");
            return JsonSerializer.Deserialize<Teams>(json);
        }

        private async Task<Models.FootballData.Team> GetFootballDataTeam(int teamID)
        {
            string json = await _httpAPIClient.Get($"{_httpAPIClient._Client.BaseAddress}teams/{teamID}");
            return JsonSerializer.Deserialize<Models.FootballData.Team>(json);
        }
    }
}
