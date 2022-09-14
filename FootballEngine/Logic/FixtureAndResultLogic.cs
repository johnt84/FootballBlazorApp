using FootballEngine.Logic.Interfaces;
using FootballShared.Models;
using FootballShared.Models.FootballData;
using static FootballShared.Models.Enums;

namespace FootballEngine.Services
{
    public class FixtureAndResultLogic : IFixtureAndResultLogic
    {
        private readonly FootballDataModel _fixturesAndResultsFootballDataModel;
        private readonly FootballEngineInput _footballEngineInput;

        public FixtureAndResultLogic(FootballDataModel fixturesAndResultsFootballDataModel, FootballEngineInput footballEngineInput)
        {
            _fixturesAndResultsFootballDataModel = fixturesAndResultsFootballDataModel;
            _footballEngineInput = footballEngineInput;
        }

        public async Task<List<FixturesAndResultsByDay>> GetFixturesAndResultsByDayAsync()
        {
            var fixturesAndResults = await GetFixtureAndResultsAsync();

            return fixturesAndResults
                        .GroupBy(x => x.FixtureDate.Date)
                        .Select(x => new FixturesAndResultsByDay()
                        {
                            FixtureDate = x.Key,
                            FixturesAndResults = x.ToList(),
                        })
                        .ToList();
        }

        public async Task<List<GroupOrLeagueTableModel>> GetFixturesAndResultsByGroupsOrLeagueTableAsync(List<GroupOrLeagueTableModel> groups)
        {
            var fixturesAndResults = await GetFixtureAndResultsAsync();

            var fixturesAndResultsByGroupsOrLeagueTable = fixturesAndResults
                                                            .GroupBy(x => x.GroupOrLeagueTable.Name)
                                                            .Select(x => new FixturesAndResultsByGroupOrLeagueTable()
                                                            {
                                                                GroupName = x.Key,
                                                                FixturesAndResults = x.ToList(),
                                                            })
                                                            .ToList();

            fixturesAndResultsByGroupsOrLeagueTable
                            .ForEach(x => x.FixturesAndResultsByDays = x.FixturesAndResults
                                                                        .GroupBy(x => x.FixtureDate.Date)
                                                                        .Select(x => new FixturesAndResultsByDay(){FixtureDate = x.Key,FixturesAndResults = x.ToList(),})
                                                                        .ToList());

            const string GROUP = "Group";

            groups
                .ForEach(x => x.FixturesAndResultsByGroupOrLeagueTable = fixturesAndResultsByGroupsOrLeagueTable
                                                                          .Where(y => y.GroupName == $"{GROUP} {x.Name}")
                                                                          .FirstOrDefault());

            return groups;
        }

        public async Task<FootballShared.Models.Team> GetFixturesAndResultsByTeamAsync(FootballShared.Models.Team team)
        {
            var fixturesAndResults = await GetFixtureAndResultsAsync();

            var teamsFixtures = fixturesAndResults
                                    .Where(x => x.HomeTeam.Name == team.Name || x.AwayTeam.Name == team.Name)
                                    .ToList();

            var teamsFixturesAndResultsByDays = teamsFixtures
                                                .GroupBy(x => x.FixtureDate.Date)
                                                .Select(x => new FixturesAndResultsByDay() 
                                                { 
                                                    FixtureDate = x.Key,
                                                    FixturesAndResults = x.ToList(), 
                                                })
                                                .ToList();

            team.FixturesAndResultsByDays = teamsFixturesAndResultsByDays;

            return team;
        }

        private async Task<List<FixtureAndResult>> GetFixtureAndResultsAsync()
        {
            var fixturesAndResults = _fixturesAndResultsFootballDataModel
                                        .matches
                                        .ToList()
                                        .Select(x => GetFixtureAndResultFromMatch(x, _footballEngineInput.LocalOffsetInMinutes))
                                        .ToList();

            return PopulateNotDrawnFixtures(fixturesAndResults);
        }

        private List<FixtureAndResult> PopulateNotDrawnFixtures(List<FixtureAndResult> fixturesAndResults)
        {
            var notDrawnFixturesAndResults = fixturesAndResults
                                                .Where(x => string.IsNullOrEmpty(x.HomeTeam.Name)
                                                            && string.IsNullOrEmpty(x.AwayTeam.Name))
                                                .ToList();

            var postGroupDraws = GetPostGroupDraws();

            notDrawnFixturesAndResults.ForEach(x => x.HomeTeam.Name = postGroupDraws
                                                                          .Where(y => y.FixtureDate == x.FixtureDate)
                                                                          .Select(x => x.HomeDraw)
                                                                          .FirstOrDefault());

            notDrawnFixturesAndResults.ForEach(x => x.AwayTeam.Name = postGroupDraws
                                                                          .Where(y => y.FixtureDate == x.FixtureDate)
                                                                          .Select(x => x.AwayDraw)
                                                                          .FirstOrDefault());

            fixturesAndResults
                .RemoveAll(x => string.IsNullOrEmpty(x.HomeTeam.Name)
                            && string.IsNullOrEmpty(x.AwayTeam.Name));

            return fixturesAndResults;
        }

        private FixtureAndResult GetFixtureAndResultFromMatch(Match match, int timeZoneOffsetInMinutes)
        {
            return new FixtureAndResult()
            {
                GameStatus = GetGameStatus(match.status),
                HomeTeam = new FootballShared.Models.Team()
                {
                    TeamID = match.homeTeam.id.HasValue ? match.homeTeam.id.Value : 0,
                    Name = match.homeTeam.shortName,
                },
                AwayTeam = new FootballShared.Models.Team()
                {
                    TeamID = match.awayTeam.id.HasValue ? match.awayTeam.id.Value : 0,
                    Name = match.awayTeam.shortName,
                },
                FixtureDate = match.utcDate.AddMinutes(-timeZoneOffsetInMinutes),
                Stage = GetStage(match.stage),
                GroupOrLeagueTable = new GroupOrLeagueTableModel()
                {
                    Name = match.group,
                },
                HomeTeamGoals = match.score.fullTime.home.HasValue ? match.score.fullTime.home.Value : 0,
                AwayTeamGoals = match.score.fullTime.away.HasValue ? match.score.fullTime.away.Value : 0,
                Result = GetResult(match.score?.winner ?? string.Empty)
            };
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
                    result = Result.Home_Win;
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

        private List<PostGroupDraw> GetPostGroupDraws()
        {
            return new List<PostGroupDraw>()
            {
                new PostGroupDraw()
                {
                    Stage = Stage.Round_of_16,
                    FixtureDate = new DateTime(2021,6,26,17,0,0),
                    HomeDraw = "Group A Second Place",
                    AwayDraw = "Group B Second Place",
                },
                new PostGroupDraw()
                {
                    Stage = Stage.Round_of_16,
                    FixtureDate = new DateTime(2021,6,26,20,0,0),
                    HomeDraw = "Group A Winner",
                    AwayDraw = "Group C Second Place",
                },
                new PostGroupDraw()
                {
                    Stage = Stage.Round_of_16,
                    FixtureDate = new DateTime(2021,6,27,17,0,0),
                    HomeDraw = "Group C Winner",
                    AwayDraw = "Third Place Group D/E/F",
                },
                new PostGroupDraw()
                {
                    Stage = Stage.Round_of_16,
                    FixtureDate = new DateTime(2021,6,27,20,0,0),
                    HomeDraw = "Group B Winner",
                    AwayDraw = "Third Place Group A/D/E/F",
                },
                new PostGroupDraw()
                {
                    Stage = Stage.Round_of_16,
                    FixtureDate = new DateTime(2021,6,28,17,0,0),
                    HomeDraw = "Group D Second Place",
                    AwayDraw = "Group E Second Place",
                },
                new PostGroupDraw()
                {
                    Stage = Stage.Round_of_16,
                    FixtureDate = new DateTime(2021,6,28,20,0,0),
                    HomeDraw = "Group F Winner",
                    AwayDraw = "Third Place Group A/B/C",
                },
                new PostGroupDraw()
                {
                    Stage = Stage.Round_of_16,
                    FixtureDate = new DateTime(2021,6,29,17,0,0),
                    HomeDraw = "Group D Winner",
                    AwayDraw = "Group F Second Place",
                },
                new PostGroupDraw()
                {
                    Stage = Stage.Round_of_16,
                    FixtureDate = new DateTime(2021,6,29,20,0,0),
                    HomeDraw = "Group E Winner",
                    AwayDraw = "Third Place Group A/B/C/D",
                },
                new PostGroupDraw()
                {
                    Stage = Stage.Quarter_Final,
                    FixtureDate = new DateTime(2021,7,2,17,0,0),
                    HomeDraw = "Winner Round of 16 match 6",
                    AwayDraw = "Winner Round of 16 match 5",
                },
                new PostGroupDraw()
                {
                    Stage = Stage.Quarter_Final,
                    FixtureDate = new DateTime(2021,7,2,20,0,0),
                    HomeDraw = "Winner Round of 16 match 4",
                    AwayDraw = "Winner Round of 16 match 2",
                },
                new PostGroupDraw()
                {
                    Stage = Stage.Quarter_Final,
                    FixtureDate = new DateTime(2021,7,3,17,0,0),
                    HomeDraw = "Winner Round of 16 match 3",
                    AwayDraw = "Winner Round of 16 match 1",
                },
                new PostGroupDraw()
                {
                    Stage = Stage.Quarter_Final,
                    FixtureDate = new DateTime(2021,7,3,20,0,0),
                    HomeDraw = "Winner Round of 16 match 8",
                    AwayDraw = "Winner Round of 16 match 7",
                },
                new PostGroupDraw()
                {
                    Stage = Stage.Semi_Final,
                    FixtureDate = new DateTime(2021,7,6,20,0,0),
                    HomeDraw = "Winner Quarter Final 2",
                    AwayDraw = "Winner Quarter Final 1",
                },
                new PostGroupDraw()
                {
                    Stage = Stage.Semi_Final,
                    FixtureDate = new DateTime(2021,7,7,20,0,0),
                    HomeDraw = "Winner Quarter Final 4",
                    AwayDraw = "Winner Quarter Final 3",
                },
                new PostGroupDraw()
                {
                    Stage = Stage.Final,
                    FixtureDate = new DateTime(2021,7,11,20,0,0),
                    HomeDraw = "Winner Semi Final 1",
                    AwayDraw = "Winner Semi Final 2",
                },
            };
        }
    }
}
