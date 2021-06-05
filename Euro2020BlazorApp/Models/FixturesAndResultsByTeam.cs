using System.Collections.Generic;

namespace Euro2020BlazorApp.Models
{
    public class FixturesAndResultsByTeam
    {
        public string teamName { get; set; }
        public Team Team { get; set; }
        public List<FixturesAndResultsByDay> FixturesAndResultsByDays { get; set; }
        public List<FixtureAndResult> FixturesAndResults { get; set; }
    }
}
