using System;
using static Euro2020BlazorApp.Models.Enums.Enums;

namespace Euro2020BlazorApp.Models
{
    public class PostGroupDraw
    {
        public Stage Stage { get; set; }
        public DateTime FixtureDate { get; set; }
        public string HomeDraw { get; set; }
        public string AwayDraw { get; set; }
    }
}
