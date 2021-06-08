using System.Collections.Generic;

namespace Euro2020BlazorApp.Models
{
    public class PlayerByPosition
    {
        public string Position { get; set; }
        public int SortOrder { get; set; }
        public List<Player> Players { get; set; }
    }
}
