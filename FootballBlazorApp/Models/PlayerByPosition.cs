using System.Collections.Generic;

namespace FootballBlazorApp.Models
{
    public class PlayerByPosition
    {
        public string Position { get; set; }
        public int SortOrder { get; set; }
        public List<Player> Players { get; set; }
    }
}
