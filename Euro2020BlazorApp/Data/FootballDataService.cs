using Euro2020BlazorApp.API;
using Euro2020BlazorApp.Models;
using Euro2020BlazorApp.Models.FootballData;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
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

        public FootballDataService(HttpAPIClient httpAPIClient, ITimeZoneOffsetService timeZoneOffsetService, IConfiguration configuration, FootballDataState footballDataState)
        {
            _httpAPIClient = httpAPIClient;
            _timeZoneOffsetService = timeZoneOffsetService;
            _configuration = configuration;
            _footballDataState = footballDataState;
        }

        public async Task<List<Group>> GetGroups()
        {
            if(_footballDataState != null && _footballDataState.Groups != null)
            {
                return _footballDataState.Groups;
            }
            
            var footballDataStandings = await GetFootballDataStandings();

            var groupService = new GroupService(footballDataStandings);
            var groups = groupService.GetGroups();

            var fixturesAndGroups = await GetFixturesAndResultsByGroups(groups);

            _footballDataState.Groups = fixturesAndGroups;

            return fixturesAndGroups;
        }

        public async Task<List<Models.Team>> GetTeams()
        {
            if (_footballDataState != null && _footballDataState.Teams != null)
            {
                return _footballDataState.Teams;
            }

            var footballDataTeams = await GetFootballDataTeams();

            var teamService = new TeamService(footballDataTeams);

            var teams = teamService.GetTeams();

            _footballDataState.Teams = teams;

            return teams;
        }

        public async Task<Models.Team> GetTeam(int teamID)
        {
            var footballDataTeam = await GetFootballDataTeam(teamID);

            var teamService = new TeamService(footballDataTeam);
            var team = teamService.GetTeam();

            FootballDataModel footballDataMatches = null;

            if (_footballDataState != null && _footballDataState.FootballDataModel != null)
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
            if (_footballDataState != null && _footballDataState.FixturesAndResultsByDays != null)
            {
                return _footballDataState.FixturesAndResultsByDays;
            }

            var footballDataMatches = await GetFootballDataMatches();

            var fixtureAndResultService = new FixtureAndResultService(footballDataMatches, _timeZoneOffsetService);

            var fixturesAndResultsByDay = await fixtureAndResultService.GetFixturesAndResultsByDay();

            _footballDataState.FixturesAndResultsByDays = fixturesAndResultsByDay;
            _footballDataState.FootballDataModel = footballDataMatches;

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
