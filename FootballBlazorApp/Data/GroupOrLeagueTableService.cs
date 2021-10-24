using FootballBlazorApp.Models;
using FootballBlazorApp.Models.FootballData;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FootballBlazorApp.Data
{
    public class GroupOrLeagueTableService
    {
        private readonly FootballDataModel _groupsFootballDataModel;
        private readonly IConfiguration _configuration;

        const string GROUP_ = "GROUP_";

        public GroupOrLeagueTableService(FootballDataModel groupsFootballDataModel, IConfiguration configuration)
        {
            _groupsFootballDataModel = groupsFootballDataModel;
            _configuration = configuration;
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
            bool hasGroups = Convert.ToBoolean(_configuration["HasGroups"].ToString());

            return new GroupOrLeagueTableModel()
            {
                Name = hasGroups ? standing.group?.Replace(GROUP_, string.Empty) : $"{_configuration["LeagueName"].ToString()} Table",
                IsGroup = hasGroups,
                GroupOrLeagueTableStandings = standing.table.ToList().Select(y => new GroupOrLeagueTableStanding()
                {
                    Team = new Models.Team()
                    {
                        TeamID = y.team.id,
                        Name = y.team.name,
                        TeamCrestUrl = y.team.crestUrl,
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
