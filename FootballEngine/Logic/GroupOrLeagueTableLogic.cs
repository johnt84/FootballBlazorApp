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
                            .Select(x => GetGroupOrLeagueTableFromStanding(_groupsFootballDataModel.competition.emblem, x))
                            .ToList();
        }

        private GroupOrLeagueTableModel GetGroupOrLeagueTableFromStanding(string emblem, Standing standing)
        {
            return new GroupOrLeagueTableModel()
            {
                Name = _footballEngineInput.SelectedCompetition.HasGroups 
                            ? standing.group?.Replace(GROUP_, string.Empty) 
                            : $"{_footballEngineInput.SelectedCompetition.CompetitionName} Table",
                Emblem = emblem,
                IsGroup = _footballEngineInput.SelectedCompetition.HasGroups,
                GroupOrLeagueTableStandings = standing.table.ToList().Select(x => new GroupOrLeagueTableStanding()
                {
                    Position = x.position,
                    Team = new FootballShared.Models.Team()
                    {
                        TeamID = x.team.id,
                        Name = x.team.shortName,
                        TeamCrestUrl = x.team.crest,
                    },
                    GamesPlayed = x.playedGames,
                    GamesWon = x.won,
                    GamesDrawn = x.draw,
                    GamesLost = x.lost,
                    GoalsScored = x.goalsFor,
                    GoalsAgainst = x.goalsAgainst,
                    GoalDifference = x.goalDifference,
                    PointsTotal = x.points,
                    Form = !string.IsNullOrWhiteSpace(x.form) 
                                ? string.Join(",", x.form.Split(",").Reverse()) //Reversed order of form guide so form guide order is in ascending order from the earliest game
                                : x.form, 
                })
                .ToList(),
            };
        }
    }
}
