using Euro2020BlazorApp.API;
using Euro2020BlazorApp.Models;
using Euro2020BlazorApp.Models.FootballData;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace Euro2020BlazorApp.Data
{
    public class FootballDataService : IFootballDataService
    {
        private readonly HttpAPIClient _httpAPIClient;
        private readonly ITimeZoneOffsetService _timeZoneOffsetService;

        public FootballDataService(HttpAPIClient httpAPIClient, ITimeZoneOffsetService timeZoneOffsetService)
        {
            _httpAPIClient = httpAPIClient;
            _timeZoneOffsetService = timeZoneOffsetService;
        }

        public async Task<List<Group>> GetGroups()
        {
            var footballDataStandings = await GetFootballDataStandings();

            var groupService = new GroupService(footballDataStandings);
            var groups = groupService.GetGroups();

            return await GetFixturesAndResultsByGroups(groups);
        }

        public async Task<List<FixturesAndResultsByDay>> GetFixturesAndResultsByDays()
        {
            var footballDataMatches = await GetFootballDataMatches();

            var fixtureAndResultService = new FixtureAndResultService(footballDataMatches, _timeZoneOffsetService);
            return await fixtureAndResultService.GetFixturesAndResultsByDay();
        }

        public async Task<List<Group>> GetFixturesAndResultsByGroups(List<Group> groups)
        {
            var footballDataMatches = await GetFootballDataMatches();

            var fixtureAndResultService = new FixtureAndResultService(footballDataMatches, _timeZoneOffsetService);
            return await fixtureAndResultService.GetFixturesAndResultsByGroups(groups);
        }

        private async Task<FootballDataModel> GetFootballDataMatches()
        {
            string json = await _httpAPIClient.Get($"{ _httpAPIClient._Client.BaseAddress }matches/");
            return JsonSerializer.Deserialize<FootballDataModel>(json);
        }

        private async Task<FootballDataModel> GetFootballDataStandings()
        {
            string json = await _httpAPIClient.Get($"{ _httpAPIClient._Client.BaseAddress }standings/");
            return JsonSerializer.Deserialize<FootballDataModel>(json);
        }

        private async Task<FootballDataModel> GetTeam(int teamID)
        {
            string json = await _httpAPIClient.Get($"{ _httpAPIClient._Client.BaseAddress }teams/{teamID}");
            return JsonSerializer.Deserialize<FootballDataModel>(json);
        }
    }
}
