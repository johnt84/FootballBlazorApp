using System.Collections.Generic;
using static FootballShared.Models.Enums.Enums;

namespace FootballShared.Models
{
    public class Team
    {
        public int TeamID { get; set; }
        public string Name { get; set; }
        public string TeamCrestUrl { get; set; }
        public int YearFounded { get; set; }
        public string Website { get; set; }
        public string TeamColours { get; set; }
        public string HomeStadium { get; set; }
        public List<Player> Squad { get; set; }
        public List<PlayerByPosition> SquadByPosition { get; set; }
        public Coach Coach { get; set; }
        public Stage StageReached { get; set; }
        public List<FixturesAndResultsByDay> FixturesAndResultsByDays { get; set; }
        public int CurrentLeaguePosition { get; set; }
    }
}
