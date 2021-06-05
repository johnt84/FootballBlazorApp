using System;
using static Euro2020BlazorApp.Models.Enums.Enums;

namespace Euro2020BlazorApp.Models
{
    public class FixtureAndResult
    {
        public GameStatus GameStatus { get; set; }
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public DateTime FixtureDate { get; set; }
        public Stage Stage { get; set; }
        public Group Group { get; set; }
        public int HomeTeamGoals { get; set; }
        public int AwayTeamGoals { get; set; }
        public Result Result { get; set; }
    }
}
