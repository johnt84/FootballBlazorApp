using System.Collections.Generic;

namespace FootballBlazorApp.Models
{
    public class FixturesAndResultsByTeam
    {
        public string TeamName { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public Team Team { get; set; }
        public List<FixturesAndResultsByDay> FixturesAndResultsByDays { get; set; }
        public List<FixtureAndResult> FixturesAndResults { get; set; }
    }
}
