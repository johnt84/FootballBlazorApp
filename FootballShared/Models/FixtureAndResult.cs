using System;
using static FootballShared.Models.Enums.Enums;

namespace FootballShared.Models
{
    public class FixtureAndResult
    {
        public GameStatus GameStatus { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public DateTime FixtureDate { get; set; }
        public Stage Stage { get; set; }
        public GroupOrLeagueTableModel GroupOrLeagueTable { get; set; }
        public int HomeTeamGoals { get; set; }
        public int AwayTeamGoals { get; set; }
        public Result Result { get; set; }
    }
}
