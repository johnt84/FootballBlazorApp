namespace FootballShared.Models
{
    public class FootballEngineInput
    {
        public string FootballDataAPIUrl { get; set; }
        public string Competition { get; set; }
        public bool IsCupCompetition { get; set; }
        public bool HasGroups { get; set; }
        public bool HasThirdPlaceRanking { get; set; }
        public string LeagueName { get; set; }
        public string Title { get; set; }
        public string APIToken { get; set; }
        public int HoursUntilRefreshCache { get; set; } = 0;
        public int LocalOffsetInMinutes { get; set; } = 0;
        public int MinutesUntilRefreshPlayerSearchCache { get; set; } = 0;
    }
}