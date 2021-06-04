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
            string json = await _httpAPIClient.Get($"{ _httpAPIClient._Client.BaseAddress }standings/");
            var model = JsonSerializer.Deserialize<FootballDataModel>(json);

            var groupService = new GroupService(model);
            return groupService.GetGroups();
        }

        public async Task<List<FixturesByDay>> GetFixtures()
        {
            string json = await _httpAPIClient.Get($"{ _httpAPIClient._Client.BaseAddress }matches/");
            var model = JsonSerializer.Deserialize<FootballDataModel>(json);

            var fixtureService = new FixtureService(model, _timeZoneOffsetService);
            return await fixtureService.GetFixtures();
        }
    }
}
