namespace FootballShared.Models
{
    public class FootballEngineInput
    {
        public string FootballDataAPIUrl { get; set; }
        public List<Competition> AvailableCompetitions { get; set; }
        public Competition SelectedCompetition { get; set; }
        public string APIToken { get; set; }
        public int HoursUntilRefreshCache { get; set; } = 0;
        public int LocalOffsetInMinutes { get; set; } = 0;
        public int MinutesUntilRefreshPlayerSearchCache { get; set; } = 0;
        public ForceCacheRefreshInput ForceCacheRefreshInput { get; set; }
    }
}