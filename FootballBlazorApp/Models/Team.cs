using System.Collections.Generic;
using static FootballBlazorApp.Models.Enums.Enums;

namespace FootballBlazorApp.Models
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
        public List<Player> CoachingStaff { get; set; }
        public List<PlayerByPosition> SquadByPosition { get; set; }
        public Stage StageReached { get; set; }
        public List<FixturesAndResultsByDay> FixturesAndResultsByDays { get; set; }
    }
}
