using static FootballShared.Models.Enums;

namespace FootballShared.Models
{
    public class Team
    {
        public int TeamID { get; set; }
        public string Name { get; set; } = null!;
        public bool IsCupCompetition { get; set; }
        public bool? IsEliminated { get; set; }
        public string? CupStage { get; set; }
        public string TeamCrestUrl { get; set; } = null!;
        public int? YearFounded { get; set; }
        public string Website { get; set; } = null!;
        public string TeamColours { get; set; } = null!;
        public string HomeStadium { get; set; } = null!;
        public List<Player> Squad { get; set; } = null!;
        public List<PlayerByPosition> SquadByPosition { get; set; } = null!;
        public Coach Coach { get; set; } = null!;
        public Stage StageReached { get; set; }
        public List<FixturesAndResultsByDay> FixturesAndResultsByDays { get; set; } = null!;
        public int CurrentLeaguePosition { get; set; }
    }
}
