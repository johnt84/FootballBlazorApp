using System.Collections.Generic;

namespace FootballShared.Models
{
    public class FixturesAndResultsByGroupOrLeagueTable
    {
        public string GroupName { get; set; }
        public GroupOrLeagueTableModel Group { get; set; }
        public List<FixturesAndResultsByDay> FixturesAndResultsByDays { get; set; }
        public List<FixtureAndResult> FixturesAndResults { get; set; }
    }
}
