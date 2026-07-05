namespace FootballShared.Models
{
    public class FootballEngineInput
    {
        public string FootballDataAPIUrl { get; set; } = null!;
        public string Competition { get; set; } = null!;
        public bool IsCupCompetition { get; set; }
        public bool HasGroups { get; set; }
        public bool HasThirdPlaceRanking { get; set; }
        public string LeagueName { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string APIToken { get; set; } = null!;
        public int HoursUntilRefreshCache { get; set; } = 0;
        public int LocalOffsetInMinutes { get; set; } = 0;
        public int MinutesUntilRefreshPlayerSearchCache { get; set; } = 0;
    }
}