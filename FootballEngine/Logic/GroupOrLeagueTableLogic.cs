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

        const int ThirdPlace = 3;

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

        public void BuildEurosThirdPlaceRankingTable(List<GroupOrLeagueTableModel> groups)
        {
            if (!groups.All(group => group.IsGroup))
            {
                return;
            }
            
            var thirdPlaceGroup = GetThirdPlaceRankingTeams(groups);

            SetThirdPlaceRankingPositions(thirdPlaceGroup.GroupOrLeagueTableStandings);

           groups.Add(thirdPlaceGroup);
        }

        private GroupOrLeagueTableModel GetGroupOrLeagueTableFromStanding(string emblem, Standing standing)
        {
            return new GroupOrLeagueTableModel()
            {
                Name = _footballEngineInput.HasGroups 
                            ? standing.group?.Replace(GROUP_, string.Empty) 
                            : $"{_footballEngineInput.LeagueName} Table",
                Emblem = emblem,
                IsGroup = _footballEngineInput.HasGroups,
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

        private GroupOrLeagueTableModel GetThirdPlaceRankingTeams(List<GroupOrLeagueTableModel> groups)
        {
            var thirdPlaceGroup = new GroupOrLeagueTableModel
            {
                Name = "Third Place Rankings",
                IsGroup = true,
                GroupOrLeagueTableStandings = new List<GroupOrLeagueTableStanding>()
            };

            var standings = new List<GroupOrLeagueTableStanding>();

            foreach (var group in groups)
            {
                var thirdPlaceTeam = group.GroupOrLeagueTableStandings
                                        .Where(standing => standing.Position == ThirdPlace)
                                        .FirstOrDefault();

                if (thirdPlaceTeam != null)
                {
                    thirdPlaceGroup.GroupOrLeagueTableStandings.Add(thirdPlaceTeam);
                }
            }

            thirdPlaceGroup.GroupOrLeagueTableStandings = thirdPlaceGroup.GroupOrLeagueTableStandings
                                                .OrderByDescending(standing => standing.PointsTotal)
                                                .ThenByDescending(standing => standing.GoalDifference)
                                                .ThenByDescending(standing => standing.GoalsScored)
                                                .ToList();

            return thirdPlaceGroup;
        }

        private void SetThirdPlaceRankingPositions(List<GroupOrLeagueTableStanding> thirdRankingPlaceStandings)
        {
            int currentPosition = 0;

            foreach (var standing in thirdRankingPlaceStandings)
            {
                standing.Position = currentPosition + 1;

                currentPosition++;
            }
        }
    }
}
