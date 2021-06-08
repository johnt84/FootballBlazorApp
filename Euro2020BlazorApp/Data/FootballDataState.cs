using Euro2020BlazorApp.Models;
using Euro2020BlazorApp.Models.FootballData;
using System.Collections.Generic;

namespace Euro2020BlazorApp.Data
{
    public class FootballDataState
    {
        public List<Group> Groups { get; set; }
        public List<Models.Team> Teams { get; set; }
        public List<FixturesAndResultsByDay> FixturesAndResultsByDays { get; set; }
        public List<Group> FixturesAndResultsByGroups { get; set; }
        public FootballDataModel FootballDataModel { get; set; }
    }
}
