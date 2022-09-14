using static FootballShared.Models.Enums;

namespace FootballShared.Models
{
    public class Player
    {
        public int PlayerID { get; set; }
        public SquadRole SquadRole { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Nationality { get; set; }
        public int Age { get; set; }
        public int SquadSortOrder { get; set; }
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public int TeamCurrentPosition { get; set; }
        public string ConfederationForDisplay { get; set; }
    }
}
