using Euro2020BlazorApp.Models;
using Euro2020BlazorApp.Models.FootballData;
using System.Collections.Generic;
using System.Linq;

namespace Euro2020BlazorApp.Data
{
    public class GroupService
    {
        Model _model;

        const string GROUP_ = "GROUP_";

        public GroupService(Model model)
        {
            _model = model;
        }

        public List<Group> GetGroups()
        {
            return _model
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
    }
}
