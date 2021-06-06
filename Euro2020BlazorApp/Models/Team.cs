﻿using System.Collections.Generic;
using static Euro2020BlazorApp.Models.Enums.Enums;

namespace Euro2020BlazorApp.Models
{
    public class Team
    {
        public int TeamID { get; set; }
        public string Name { get; set; }
        public string FlagIcon { get; set; }
        public int YearFounded { get; set; }
        public string Website { get; set; }
        public string TeamColours { get; set; }
        public string HomeStadium { get; set; }
        public List<Player> Squad { get; set; }
        public Stage StageReached { get; set; }
        public FixturesAndResultsByTeam FixturesAndResultsByTeam { get; set; }
        public List<FixturesAndResultsByDay> FixturesAndResultsByDays { get; set; }
    }
}
