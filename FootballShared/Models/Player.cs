using System;
using static FootballShared.Models.Enums.Enums;

namespace FootballShared.Models
{
    public class Player
    {
        public int PlayerID { get; set; }
        public SquadRole SquadRole { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CountryOfBirth { get; set; }
        public string Nationality { get; set; }
        public int? ShirtNumber { get; set; }
        public int Age { get; set; }
        public int SquadSortOrder { get; set; }
        public int TeamID { get; set; }
    }
}
