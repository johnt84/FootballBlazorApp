using Euro2020BlazorApp.Models;
using Euro2020BlazorApp.Models.FootballData;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Euro2020BlazorApp.Models.Enums.Enums;

namespace Euro2020BlazorApp.Data
{
    public class FixtureAndResultService
    {
        private readonly FootballDataModel _fixturesAndResultsFootballDataModel;
        private readonly ITimeZoneOffsetService _timeZoneOffsetService;

        public FixtureAndResultService(FootballDataModel fixturesAndResultsFootballDataModel, ITimeZoneOffsetService timeZoneOffsetService)
        {
            _fixturesAndResultsFootballDataModel = fixturesAndResultsFootballDataModel;
            _timeZoneOffsetService = timeZoneOffsetService;
        }

        public async Task<List<FixturesAndResultsByDay>> GetFixturesAndResultsByDay()
        {
            var fixturesAndResults = await GetFixtureAndResults();

            return fixturesAndResults
                        .GroupBy(x => x.FixtureDate.Date).Select(x => new FixturesAndResultsByDay()
                        {
                            FixtureDate = x.Key,
                            FixturesAndResults = x.ToList(),
                        })
                        .ToList();
        }

        public async Task<FixturesAndResultsByGroup> GetFixturesAndResultsByGroup(Group group)
        {
            var fixturesAndResults = await GetFixtureAndResults();

            const string GROUP = "Group";

            var fixturesAndResultsByGroup = fixturesAndResults
                                                .Where(x => x.Group.Name == $"{GROUP} {group.Name}")
                                                .GroupBy(x => x.Group.Name).Select(x => new FixturesAndResultsByGroup()
                                                {
                                                    GroupName = x.Key,
                                                    FixturesAndResults = x.ToList(),
                                                })
                                                .FirstOrDefault();

            fixturesAndResultsByGroup.Group = group;

            return fixturesAndResultsByGroup;
        }

        private async Task<List<FixtureAndResult>> GetFixtureAndResults()
        {
            int timeZoneOffsetInMinutes = await _timeZoneOffsetService.GetLocalOffsetInMinutes();

            return  _fixturesAndResultsFootballDataModel
                                        .matches
                                        .ToList()
                                        .Select(x => new FixtureAndResult()
                                        {
                                            GameStatus = GetGameStatus(x.status),
                                            HomeTeam = new Models.Team()
                                            {
                                                Name = x.homeTeam.name,
                                            },
                                            AwayTeam = new Models.Team()
                                            {
                                                Name = x.awayTeam.name,
                                            },
                                            FixtureDate = x.utcDate.AddMinutes(-timeZoneOffsetInMinutes),
                                            Stage = GetStage(x.stage),
                                            Group = new Group()
                                            {
                                                Name = x.group,
                                            },
                                        })
                                        .ToList();
        }

        private Stage GetStage(string stageName)
        {
            var stage = Stage.Group;
            
            switch(stageName)
            {
                case "GROUP_STAGE":
                    stage = Stage.Group;
                    break;
                case "LAST_16":
                    stage = Stage.Round_of_16;
                    break;
                case "QUARTER_FINAL":
                    stage = Stage.Quarter_Final;
                    break;
                case "SEMI_FINAL":
                    stage = Stage.Semi_Final;
                    break;
                case "FINAL":
                    stage = Stage.Final;
                    break;
                default:
                    break;
            }

            return stage;
        }

        private GameStatus GetGameStatus(string status)
        {
            var gameStatus = GameStatus.Scheduled;

            switch (status)
            {
                case "SCHEDULED":
                    gameStatus = GameStatus.Scheduled;
                    break;
                case "IN_PLAY":
                    gameStatus = GameStatus.In_Play;
                    break;
                case "FINISHED": 
                case "AWARDED":
                    gameStatus = GameStatus.Result;
                    break;
                default:
                    break;
            }

            return gameStatus;
        }
    }
}
