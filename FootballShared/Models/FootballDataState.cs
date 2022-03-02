
using FootballShared.Models.FootballData;
using System;
using System.Collections.Generic;

namespace FootballShared.Models
{
    public class FootballDataState
    {
        public DateTime? CompetitionStartDate { get; set; }
        public FootballDataModel FootballDataMatches { get; set; }
        public FootballDataModel FootballDataStandings { get; set; }
        public List<Models.Team> Teams { get; set; }
        public DateTime LastRefreshTime { get; set; }
        public bool IsCacheRefreshed { get; set; } = false;
    }
}
