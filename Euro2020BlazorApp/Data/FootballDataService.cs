using Euro2020BlazorApp.API;
using Euro2020BlazorApp.Models;
using Euro2020BlazorApp.Models.FootballData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using static Euro2020BlazorApp.Models.Enums.Enums;

namespace Euro2020BlazorApp.Data
{
    public class FootballDataService : IFootballDataService
    {
        private readonly HttpAPIClient _httpAPIClient;

        public FootballDataService(HttpAPIClient httpAPIClient)
        {
            _httpAPIClient = httpAPIClient;
        }

        const string GROUP_ = "GROUP_";

        public async Task<List<Group>> GetGroups()
        {
            string json = await _httpAPIClient.Get($"{ _httpAPIClient._Client.BaseAddress }standings/");
            var model = JsonSerializer.Deserialize<Model>(json);

            return model
                        .standings
                        .ToList()
                        .Select(x => new Group()
                        {
                            Name = x.group.Replace(GROUP_, string.Empty),
                            GroupStandings = x.table.ToList().Select(y => new GroupStanding()
                            {
                                Team = new Models.Team()
                                {
                                    Name = y.team.name,
                                },
                                GamesPlayed = y.playedGames,
                                GamesWon = y.won,
                                GamesDrawn = y.draw,
                                GamesLost = y.lost,
                                GoalsScored = y.goalsFor,
                                GoalsAgainst = y.goalsAgainst,
                                GoalDifference = y.goalDifference,
                                PointsTotal = y.points,
                            })
                            .ToList(),
                        })
                        .ToList();
        }

        public async Task<List<Fixture>> GetFixtures()
        {
            string json = await _httpAPIClient.Get($"{ _httpAPIClient._Client.BaseAddress }matches/");
            var model = JsonSerializer.Deserialize<Model>(json);

            return model
                        .matches
                        .ToList()
                        .Select(x => new Fixture()
                        {
                            HomeTeam = new Models.Team()
                            {
                                Name = x.homeTeam.name,
                            },
                            AwayTeam = new Models.Team()
                            {
                                Name = x.awayTeam.name,
                            },
                            FixtureDate = x.utcDate,
                            Stage = Enum.Parse<Stage>(x.stage),
                            Group = new Group()
                            {
                                Name = x.group,
                            },
                        })
                        .ToList();
        }
    }
}
