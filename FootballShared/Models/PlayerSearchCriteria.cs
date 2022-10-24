namespace FootballShared.Models
{
    public class PlayerSearchCriteria
    {
        public string PlayerName { get; set; }
        public string TeamName { get; set; }
        public int? PlayerAgeMinimum { get; set; }
        public int? PlayerAgeMaximum { get; set; }
        public string PlayerNationality { get; set; }
        public List<string> PlayerPositions { get; set; }
        public int? TeamPositionMinimum { get; set; }
        public int? TeamPositionMaximum { get; set; }
        public List<string> PlayerConfederations { get; set; }
    }
}
