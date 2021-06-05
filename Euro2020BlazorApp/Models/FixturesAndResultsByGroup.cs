using System.Collections.Generic;

namespace Euro2020BlazorApp.Models
{
    public class FixturesAndResultsByGroup
    {
        public string GroupName { get; set; }
        public Group Group { get; set; }
        public List<FixturesAndResultsByDay> FixturesAndResultsByDays { get; set; }
        public List<FixtureAndResult> FixturesAndResults { get; set; }
    }
}
