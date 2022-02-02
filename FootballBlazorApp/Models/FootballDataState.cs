
using FootballBlazorApp.Models.FootballData;
using System;
using System.Collections.Generic;

namespace FootballBlazorApp.Models
{
    public class FootballDataState
    {
        public DateTime? CompetitionStartDate { get; set; }
        public int CurrentMatchday { get; set; }
        public FootballDataModel FootballDataMatches { get; set; }
        public FootballDataModel FootballDataStandings { get; set; }
        public List<Models.Team> Teams { get; set; }
        public DateTime LastRefreshTime { get; set; }
    }
}
