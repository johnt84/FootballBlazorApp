using System;
using static FootballBlazorApp.Models.Enums.Enums;

namespace FootballBlazorApp.Models
{
    public class PostGroupDraw
    {
        public Stage Stage { get; set; }
        public DateTime FixtureDate { get; set; }
        public string HomeDraw { get; set; }
        public string AwayDraw { get; set; }
    }
}
