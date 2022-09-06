using FootballEngine.Logic.Interfaces;
using FootballShared.Models;
using FootballShared.Models.FootballData;

namespace FootballEngine.Services
{
    public class GroupOrLeagueTableLogic : IGroupOrLeagueTableLogic
    {
        private readonly FootballDataModel _groupsFootballDataModel;
        private readonly FootballEngineInput _footballEngineInput;

        const string GROUP_ = "GROUP_";

        public GroupOrLeagueTableLogic(FootballDataModel groupsFootballDataModel, FootballEngineInput footballEngineInput)
        {
            _groupsFootballDataModel = groupsFootballDataModel;
            _footballEngineInput = footballEngineInput;
        }

        public List<GroupOrLeagueTableModel> GetGroupsOrLeagueTable()
        {
            return _groupsFootballDataModel
                            .standings
                            .ToList()
                            .Select(x => GetGroupOrLeagueTableFromStanding(x))
                            .ToList();
        }

        private GroupOrLeagueTableModel GetGroupOrLeagueTableFromStanding(Standing standing)
        {
            return new GroupOrLeagueTableModel()
            {
                Name = _footballEngineInput.HasGroups ? standing.group?.Replace(GROUP_, string.Empty) : $"{_footballEngineInput.LeagueName} Table",
                IsGroup = _footballEngineInput.HasGroups,
                GroupOrLeagueTableStandings = standing.table.ToList().Select(y => new GroupOrLeagueTableStanding()
                {
                    Team = new FootballShared.Models.Team()
                    {
                        TeamID = y.team.id,
                        Name = y.team.name,
                        TeamCrestUrl = y.team.crest,
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
            };
        }
    }
}
