
using Euro2020BlazorApp.Models.FootballData;
using System;
using System.Collections.Generic;

namespace Euro2020BlazorApp.Models
{
    public class FootballDataState
    {
        public DateTime? CompetitionStartDate { get; set; }
        public List<Group> Groups { get; set; }
        public List<Models.Team> Teams { get; set; }
        public List<FixturesAndResultsByDay> FixturesAndResultsByDays { get; set; }
        public List<Group> FixturesAndResultsByGroups { get; set; }
        public FootballDataModel FootballDataModel { get; set; }
        public DateTime LastRefreshTime { get; set; }
    }
}
