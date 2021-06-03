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

        public FootballDataService(HttpAPIClient httpAPIClient)
        {
            _httpAPIClient = httpAPIClient;
        }

        public async Task<List<Group>> GetGroups()
        {
            string json = await _httpAPIClient.Get($"{ _httpAPIClient._Client.BaseAddress }standings/");
            var model = JsonSerializer.Deserialize<Model>(json);

            var groupService = new GroupService(model);
            return groupService.GetGroups();
        }

        public async Task<List<Fixture>> GetFixtures()
        {
            string json = await _httpAPIClient.Get($"{ _httpAPIClient._Client.BaseAddress }matches/");
            var model = JsonSerializer.Deserialize<Model>(json);

            var fixtureService = new FixtureService(model);
            return fixtureService.GetFixtures();
        }
    }
}
