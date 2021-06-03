using System.Collections.Generic;

namespace Euro2020BlazorApp.Models
{
    public class Group
    {
        public string Name { get; set; }
        public List<GroupStanding> GroupStandings { get; set; }
    }
}
