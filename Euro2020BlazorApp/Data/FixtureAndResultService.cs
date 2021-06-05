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

        public async Task<List<Group>> GetFixturesAndResultsByGroups(List<Group> groups)
        {
            var fixturesAndResults = await GetFixtureAndResults();

            var fixturesAndResultsByGroups = fixturesAndResults
                                                .GroupBy(x => x.Group.Name).Select(x => new FixturesAndResultsByGroup()
                                                {
                                                    GroupName = x.Key,
                                                    FixturesAndResults = x.ToList(),
                                                })
                                                .ToList();

            fixturesAndResultsByGroups
                            .ForEach(x => x.FixturesAndResultsByDays = x.FixturesAndResults
                                                                        .GroupBy(x => x.FixtureDate.Date)
                                                                        .Select(x => new FixturesAndResultsByDay(){FixtureDate = x.Key,FixturesAndResults = x.ToList(),})
                                                                        .ToList());

            const string GROUP = "Group";

            groups
                .ForEach(x => x.FixturesAndResultsByGroup = fixturesAndResultsByGroups
                                                              .Where(y => y.GroupName == $"{GROUP} {x.Name}")
                                                              .FirstOrDefault());

            return groups;
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
                                                TeamID = x.homeTeam.id.HasValue ? x.homeTeam.id.Value : 0,
                                                Name = x.homeTeam.name,
                                            },
                                            AwayTeam = new Models.Team()
                                            {
                                                TeamID = x.awayTeam.id.HasValue ? x.awayTeam.id.Value : 0,
                                                Name = x.awayTeam.name,
                                            },
                                            FixtureDate = x.utcDate.AddMinutes(-timeZoneOffsetInMinutes),
                                            Stage = GetStage(x.stage),
                                            Group = new Group()
                                            {
                                                Name = x.group,
                                            },
                                            HomeTeamGoals = x.score.fullTime.homeTeam.HasValue ? x.score.fullTime.homeTeam.Value : 0,
                                            AwayTeamGoals = x.score.fullTime.awayTeam.HasValue ? x.score.fullTime.awayTeam.Value : 0,
                                            Result = GetResult(x.score?.winner ?? string.Empty)
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

        private Result GetResult(string winner)
        {
            var result = Result.Draw;

            switch (winner)
            {
                case "HOME_TEAM":
                    result = Result.Away_Win;
                    break;
                case "AWAY_TEAM":
                    result = Result.Away_Win;
                    break;
                case "DRAW":
                    result = Result.Draw;
                    break;
                default:
                    break;
            }

            return result;
        }
    }
}
