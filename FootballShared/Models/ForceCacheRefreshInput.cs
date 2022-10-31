namespace FootballShared.Models
{
    public class ForceCacheRefreshInput
    {
        public bool ForceCacheRefresh { get; set; }
        public bool StandingsRefreshed { get; set; }
        public bool MatchesRefreshed { get; set; }
        public bool TeamsAndPlayersRefreshed { get; set; }
    }
}
