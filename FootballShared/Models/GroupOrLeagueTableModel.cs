namespace FootballShared.Models
{
    public class GroupOrLeagueTableModel
    {
        public string Name { get; set; } = null!;
        public string Emblem { get; set; } = null!;
        public bool IsGroup { get; set; }
        public List<GroupOrLeagueTableStanding> GroupOrLeagueTableStandings { get; set; } = null!;
        public FixturesAndResultsByGroupOrLeagueTable? FixturesAndResultsByGroupOrLeagueTable { get; set; }
    }
}
