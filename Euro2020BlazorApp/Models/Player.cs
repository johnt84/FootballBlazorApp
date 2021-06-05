using System;

namespace Euro2020BlazorApp.Models
{
    public class Player
    {
        public int PlayerID { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string CountryOfBirth { get; set; }
        public string Nationality { get; set; }
        public int? ShirtNumber { get; set; }
        public string Role { get; set; }
    }
}
