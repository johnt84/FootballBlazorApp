namespace FootballShared.Models
{
    public class GroupOrLeagueTableModel
    {
        public string Name { get; set; }
        public string Emblem { get; set; }
        public bool IsGroup { get; set; }
        public List<GroupOrLeagueTableStanding> GroupOrLeagueTableStandings { get; set; }
        public FixturesAndResultsByGroupOrLeagueTable FixturesAndResultsByGroupOrLeagueTable { get; set; }
    }
}
