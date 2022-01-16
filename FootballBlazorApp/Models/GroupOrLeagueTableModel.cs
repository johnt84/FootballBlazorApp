using System.Collections.Generic;

namespace FootballBlazorApp.Models
{
    public class GroupOrLeagueTableModel
    {
        public string Name { get; set; }
        public bool IsGroup { get; set; }
        public List<GroupOrLeagueTableStanding> GroupOrLeagueTableStandings { get; set; }
        public FixturesAndResultsByGroupOrLeagueTable FixturesAndResultsByGroupOrLeagueTable { get; set; }
    }
}
